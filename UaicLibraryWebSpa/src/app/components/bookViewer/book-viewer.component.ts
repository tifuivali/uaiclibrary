import { Component, OnInit, OnDestroy } from '@angular/core';
import { TranslateService } from "../../translations";
import { ActivatedRoute } from '@angular/router';
import { PaginationData } from '../../data/pagination.data';
import { BookService } from '../../services/book.service';
import { ErorHelper } from '../../helpers/error.helper';
import { Book } from '../../models/book';
import { ModalService } from '../../services/modal-service';
import { BookPageMark } from '../../models/book-page-mark.model';
import { Notifier } from '../../components/notifications/notifier';
import { BookAnnotation } from '../../models/book-annotation.model';

declare var viewPdf;
declare var getPdfNumberOfPages;
declare var displayPage;
declare var openPdf;

@Component({
    selector: 'book-viewer',
    templateUrl: './book-viewer.component.html',
    styleUrls: ['./book-viewer.component.css']
})
export class BookViewerComponent implements OnInit, OnDestroy {
    constructor(
        public route: ActivatedRoute,
        private bookService: BookService,
        private errorHelper: ErorHelper,
        private modalService: ModalService,
        private notifier: Notifier
    ) {
    }

    private pdfDocument: any;
    bookId: number;
    book: Book;
    paginationData: PaginationData = new PaginationData(1, 1);
    scrollSize: number = 1;
    private bookIsMarkedAsRead: boolean = false;
    private lockEditFromClick: boolean = true;
    private annotateModalId: string = "annotate-modal";
    private lastSavedPage: BookPageMark = new BookPageMark(0, 0, null);
    private bookPageAnnotation: BookAnnotation = new BookAnnotation(0, '', 0, null);
    private bookPageAnnotationContent: string = '';

    initializeBookIsMarkedAsRead() {
        this.bookService.isBookMarkedAsRead(this.book.id)
            .subscribe(isBookMarked => {
                console.log(isBookMarked);
                this.bookIsMarkedAsRead = isBookMarked;
            }, err => {

            });
    }

    initializeLastSavedPage() {
        this.bookService.getLastSavedPage(this.book.id)
            .subscribe(bookMarkPage => {
                this.lastSavedPage = bookMarkPage;
            },
            err => {
                this.lastSavedPage = new BookPageMark(this.book.id, -1, new Date());
            });
    }

    ngOnInit() {
        var viewAnnotation: string = 'false';
        this.route.params.subscribe(params => {
            this.bookId = params['bookId'];
            viewAnnotation = params['viewAnnotation'];
            console.log(params);
            var paramPage: number = parseInt(params['page']);
            this.paginationData.page = paramPage;
            var self = this;
            this.bookService.getBookById(this.bookId)
                .subscribe(book => {
                    this.book = book;
                    var self2 = this;
                    this.initializeBookIsMarkedAsRead();
                    this.initializeLastSavedPage();
                    openPdf(self.book.bookUrl).then(function (pdfDoc) {
                        self2.pdfDocument = pdfDoc;
                        viewPdf(self2.pdfDocument, 'holder', { scale: self2.scrollSize });
                        displayPage(1, self2.pdfDocument);
                        self2.paginationData.numberOfPages = getPdfNumberOfPages(self2.pdfDocument);
                        self2.onPageChanged(self2.paginationData.page);
                        if(viewAnnotation == 'true')
                        {
                            console.log('ANNOTATE');
                            self2.annotatePage();
                        }
                    });
                }, err => {
                    this.errorHelper.handleBasicError(err.status, err.errors);
                });
        });
        this.modalService.add(this.annotateModalId);
    }

    ngOnDestroy() {
        this.modalService.remove(this.annotateModalId);
    }

    onScrollSet(scroll: number) {
        this.scrollSize = scroll;
        console.log(scroll);
        viewPdf(this.pdfDocument, 'holder', { scale: this.scrollSize });
        displayPage(this.paginationData.page, this.pdfDocument);
    }

    onPageChanged(page: number) {
        displayPage(page, this.pdfDocument);
        this.paginationData.page = page;
    }

    markBookAsRead() {
        this.bookService.markBookAsRead(this.book.id)
            .subscribe(data => {
                this.bookIsMarkedAsRead = true;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    lockEditOnClick() {
        this.lockEditFromClick = true;
    }

    unlockEditOnClick() {
        this.lockEditFromClick = false;
    }

    storeUserBookPage() {
        var bookMarkpage = new BookPageMark(this.book.id, this.paginationData.page, new Date());
        this.bookService.saveBookPageMark(bookMarkpage)
            .subscribe(data => {
                this.lastSavedPage = bookMarkpage;
                this.notifier.notifySuccess("PageWasStored");
            },
            err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    annotatePage() {
        this.bookService.getBookPageAnnotation(this.book.id, this.paginationData.page)
            .subscribe(bookPageAnnotationRecived => {
                this.bookPageAnnotation = bookPageAnnotationRecived;
                this.bookPageAnnotationContent = bookPageAnnotationRecived.content;
                this.modalService.open(this.annotateModalId);
            }, err => {
                this.bookPageAnnotationContent = null;
                this.modalService.open(this.annotateModalId);
            });
    }

    viewContainerOnClick() {
        if (this.lockEditFromClick) {
            return;
        }
        this.annotatePage();
    }

    saveAnnotation(content: string) {
        var annotation = new BookAnnotation(this.book.id, content, this.paginationData.page, new Date());
        this.bookService.saveBookPageAnnotation(annotation)
            .subscribe(data => {
                this.bookPageAnnotation = annotation;
                this.notifier.notifySuccess("AnnotationSavedSuccesfully");
            },
            err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    goToLastSavedPage() {
        this.onPageChanged(this.lastSavedPage.page);
    }
}
