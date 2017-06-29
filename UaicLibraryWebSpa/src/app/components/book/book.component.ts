import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { TranslateService } from "../../translations"
import { Book } from '../../models/book';
import { BookService } from '../../services/book.service';
import { ErorHelper } from '../../helpers/error.helper';
import { Router } from "@angular/router";
import { User } from '../../models/autentification/user';
import { ModalService } from '../../services/modal-service';
import { ApiUrlHelper } from '../../http-request/http-request';
import { AuthService } from '../../services/auth.service';


@Component({
    selector: 'book',
    templateUrl: './book.component.html'
})
export class BookComponent implements OnDestroy, OnInit {
    constructor(
        private _translate: TranslateService,
        private bookService: BookService,
        private errorHelper: ErorHelper,
        private router: Router,
        private modalService: ModalService,
        private apiUrlHelper: ApiUrlHelper,
        private authService: AuthService
    ) {
        this.modalService.add(this.userModalListId);
    }

    @Input() book: Book;
    @Input() showDetails: boolean;
    likeIsEnabled = true;
    enableComments = false;
    private userModalListId: string = "book-user-likes-modal-";
    private userLikes: Array<User> = [];
    private bookDownloadUrl: string;

    ngOnDestroy() {
        this.modalService.remove(this.userModalListId);
    }

    ngOnInit() {
        this.userModalListId = this.userModalListId + this.book.id;
        this.bookDownloadUrl = this.apiUrlHelper.getUrlForObject('book/download', { bookId: this.book.id });

    }

    viewBook(book: Book) {
        this.bookService.openBook(this.book);
    }

    like() {
        if (!this.likeIsEnabled) {
            return;
        }
        this.bookService.like(this.book.id)
            .subscribe(data => {
                this.book.likes++;
                this.likeIsEnabled = false;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    dislike() {
        if (!this.likeIsEnabled) {
            return;
        }
        this.bookService.dislike(this.book.id)
            .subscribe(data => {
                this.book.dislikes++;
                this.likeIsEnabled = false;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    showComments() {
        this.enableComments = !this.enableComments
    }

    viewLikes() {
        this.bookService.getUserLikes(this.book.id)
            .subscribe(users => {
                this.userLikes = users;
                console.log(users);
                console.log(this.userLikes);
                if (users.length <= 0) return;
                this.modalService.open(this.userModalListId);
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    viewDislikes() {
        this.bookService.getUserDislikes(this.book.id)
            .subscribe(data => {
                this.userLikes = data;
                if (data.length <= 0) return;
                this.modalService.open(this.userModalListId);
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    viewBookViews() {
        this.bookService.getBookOpenners(this.book.id)
            .subscribe(users => {
                this.userLikes = users;
                if (users.length <= 0) return;
                this.modalService.open(this.userModalListId);
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    viewBookReaders() {
        this.bookService.getBookReaders(this.book.id)
            .subscribe(users => {
                this.userLikes = users;
                if (users.length <= 0) return;
                this.modalService.open(this.userModalListId);
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    editBook() {
        this.router.navigate(['editBook', this.book.id]);
    }
}
