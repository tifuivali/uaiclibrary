import { Component, OnInit } from '@angular/core';
import { AdministrationService } from '../../../services/administration.service';
import { BookAdministrationModel } from '../../../models/book-administration.model';
import { MultiSelectItem } from '../../common/multiselect/multi-select-item';
import { FacultyService } from '../../../services/faculty.service';
import { BookService } from '../../../services/book.service';
import { ErorHelper } from '../../../helpers/error.helper';
import { DropdownItem } from '../../common/dropdown/dropdown-item';
import { AuthorService } from '../../../services/author.service';
import { ActivatedRoute } from '@angular/router';
import { ArrayHelper } from '../../../helpers/array.helper';
import { Book } from '../../../models/book';
import { Notifier } from '../../notifications/notifier';
import { ApiUrlHelper } from '../../../http-request/http-request';
import { ModalService } from '../../../services/modal-service';
import { Router } from '@angular/router';

@Component({
    selector: 'book-administration',
    templateUrl: './book-administration.component.html'
})

export class BookAdministrationComponent implements OnInit {
    constructor(
        private administrationService: AdministrationService,
        private bookService: BookService,
        private facultyService: FacultyService,
        private errorHelper: ErorHelper,
        private authorService: AuthorService,
        private route: ActivatedRoute,
        private arrayHelper: ArrayHelper,
        private notifier: Notifier,
        private apiUrlHelper: ApiUrlHelper,
        private modalService: ModalService,
        private router: Router
    ) {
        this.model.imageUrl = apiUrlHelper.getBookImageUrl('default.jpg');
        this.uploadBookImageUrl = apiUrlHelper.getApiUrl() + 'bookAdministration/uploadImage';
        this.uploadBookContentUrl = apiUrlHelper.getApiUrl() + 'bookAdministration/uploadContent';
    }

    private bookId: number = 0;
    private uploadBookImageUrl: string;
    private uploadBookContentUrl: string;
    private facultiesDropDownItems: Array<DropdownItem> = [];
    private bookCategoryItems: Array<DropdownItem> = [];
    private authorsMultiSelectItems: Array<MultiSelectItem> = []
    private selectedBookCategoryItem: DropdownItem;
    private selectedBookFacultyItem: DropdownItem;
    public model: BookAdministrationModel = new BookAdministrationModel(0, null, [], null, null, 0, 0, null, 0, null, null);

    intAuthorMultiSelectData() {
        this.authorService.getAuthors()
            .subscribe(authorItems => {
                this.authorsMultiSelectItems = authorItems;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    initFacultyDropdownData() {
        this.facultyService.getAll()
            .subscribe(faculties => {
                this.facultiesDropDownItems = this.facultyService.getDropDownItemsFromFaculties(faculties);
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    initAuthorDropdownDataForBook(selecetdAuthors: Array<MultiSelectItem>) {
        this.authorService.getAuthors()
            .subscribe(authorItems => {
                console.log(authorItems);
                console.log(selecetdAuthors);
                this.authorsMultiSelectItems = authorItems;
                this.authorsMultiSelectItems = this.arrayHelper.getSelectedFromList2MultiSelectItems(this.authorsMultiSelectItems, selecetdAuthors);
                this.model.bookAuthors = this.authorsMultiSelectItems;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    initBookCategoryDropdownData() {
        this.bookService.getAllBookCategories()
            .subscribe(bookCategories => {
                this.bookCategoryItems = this.bookService.getDropdownItemBookCategories(bookCategories);
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    initDropMultiSelectLists() {
        this.initFacultyDropdownData();
        this.initBookCategoryDropdownData();
        this.intAuthorMultiSelectData();
    }

    getBookAdministrtionModelFromBook(book: Book): BookAdministrationModel {
        return new BookAdministrationModel(book.id, book.isbn, [], book.description, book.name, book.numberOfPages, 0, book.publishDate, 0, book.bookUrl, book.imageUrl);
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.bookId = params['bookId'];
            if (!this.bookId) {
                this.initDropMultiSelectLists();
                this.bookId = 0;
                return;
            }
            this.bookService.getBookById(this.bookId)
                .subscribe(book => {
                    this.model = this.getBookAdministrtionModelFromBook(book);
                    this.initFacultyDropdownData();
                    this.initBookCategoryDropdownData();
                    console.log(this.model);
                    if (book.faculty) {
                        this.selectedBookFacultyItem = new DropdownItem(book.faculty.name, book.faculty.id);
                        this.model.facultyId = book.faculty.id;
                    }
                    if (book.bookCategory) {
                        this.selectedBookCategoryItem = new DropdownItem(book.bookCategory.name, book.bookCategory.id);
                        this.model.bookCategoryId = book.bookCategory.id;
                    }
                    if (book.bookAuthors) {
                        this.initAuthorDropdownDataForBook(this.authorService.getMultiSelectItemsFromAuthors(book.bookAuthors));
                    }
                }, err => {
                    this.errorHelper.handleBasicError(err.status, err.errors);
                })
        });
    }

    selectFaculty(item: DropdownItem) {
        this.model.facultyId = item.value;
    }

    selectBookCategory(item: DropdownItem) {
        this.model.bookCategoryId = item.value;
    }

    onSubmit() {
        this.administrationService.saveBook(this.model)
            .subscribe(book => {
                this.notifier.notifySuccess("SavedModification");
                this.model.imageUrl = book.imageUrl;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    updateBookImage() {
        this.modalService.open('upload-image-modal');
    }

    updateBookFile() {
        this.modalService.open('upload-modal-content')
    }

    uploadBookImageDone(url: any) {
        this.model.imageUrl = url;
    }

    uploadBookContent(url: any) {
        this.model.bookUrl = url;
    }

    removeBook() {
        if (this.bookId <= 0) return;
        this.administrationService.removeBook(this.bookId)
            .subscribe(ok => {
                this.notifier.notifySuccess('BookWasDeleted');
                this.router.navigate(['/books']);
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

}