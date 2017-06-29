import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { ApiUrlHelper } from '../http-request/http-request';
import { Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpClient } from '../http-client';
import { Book } from '../models/book';
import { MultiSelectItem } from '../components/common/multiselect/multi-select-item';
import { BookFilter } from '../filters/book.filter';
import { BookData } from '../data/book.data';
import { BookComment } from '../models/book-comment.model';
import { User } from '../models/autentification/user';
import { Notifier } from '../components/notifications/notifier';
import { Router } from '@angular/router';
import { ErorHelper } from '../helpers/error.helper';
import { BookPageMark } from '../models/book-page-mark.model';
import { BookAnnotation } from '../models/book-annotation.model';
import { BookPageAnnotation } from '../models/bookAnnotationPage.model';
import { BookCategory } from '../models/book-category';
import { DropdownItem } from '../components/common/dropdown/dropdown-item';

@Injectable()
export class BookService {
    constructor(private httpClient: HttpClient,
        private apiUrlHelper: ApiUrlHelper,
        private notifier: Notifier,
        private router: Router,
        private errorHelper: ErorHelper
    ) { }

    getLatest(count: number): Observable<Array<Book>> {
        var url = this.apiUrlHelper.getUrlForObject('book/getLatest', { count: count });
        return this.httpClient.get(url);
    }

    getBookById(bookId: number): Observable<Book> {
        var url = this.apiUrlHelper.getUrlForObject("book/getBookById", { bookId: bookId });
        return this.httpClient.get(url);
    }

    getBookCategories(): Observable<Array<MultiSelectItem>> {
        var url = this.apiUrlHelper.getUrl('book/getCategories');
        return this.httpClient.get(url);
    }

    getAllBookCategories(): Observable<Array<BookCategory>> {
        var url = this.apiUrlHelper.getUrl('book/getCategories');
        return this.httpClient.get(url);
    }

    getFiltered(filter: BookFilter): Observable<BookData> {
        var url = this.apiUrlHelper.encodeQueryData('book/getFiltered', filter);
        return this.httpClient.get(url);
    }

    filter(filter: BookFilter): Observable<BookData> {
        var url = this.apiUrlHelper.getUrl('book/filter');
        return this.httpClient.post(url, filter);
    }

    like(bookId: number): Observable<any> {
        var url = this.apiUrlHelper.getUrl('book/like');
        return this.httpClient.post(url, bookId);
    }

    dislike(bookId: number): Observable<any> {
        var url = this.apiUrlHelper.getUrl('book/dislike');
        return this.httpClient.post(url, bookId);
    }

    getComments(bookId: number): Observable<Array<BookComment>> {
        var url = this.apiUrlHelper.getUrlForObject('book/getComments', { bookId: bookId });
        return this.httpClient.get(url);
    }

    readBook(bookId: number): Observable<any> {
        var url = this.apiUrlHelper.getUrl('book/openBook');
        return this.httpClient.post(url, bookId);
    }

    getUserLikes(bookId: number): Observable<Array<User>> {
        var url = this.apiUrlHelper.getUrlForObject('book/getBookUserLikes', { bookId: bookId });
        return this.httpClient.get(url);
    }

    getUserDislikes(bookId: number): Observable<Array<User>> {
        var url = this.apiUrlHelper.getUrlForObject('book/getBookUserDislikes', { bookId: bookId });
        return this.httpClient.get(url);
    }

    getBookOpenners(bookId: number): Observable<Array<User>> {
        var url = this.apiUrlHelper.getUrlForObject('book/getBookOpenners', { bookId: bookId });
        return this.httpClient.get(url);
    }

    markBookAsRead(bookId: number): Observable<any> {
        var url = this.apiUrlHelper.getUrlForObject('book/markAsRead', { bookId: bookId });
        return this.httpClient.get(url);
    }

    getBookReaders(bookId: number): Observable<Array<User>> {
        var url = this.apiUrlHelper.getUrlForObject('book/getReaders', { bookId: bookId });
        return this.httpClient.get(url);
    }

    isBookMarkedAsRead(bookId: number): Observable<boolean> {
        var url = this.apiUrlHelper.getUrlForObject('book/isBookMarkedARead', { bookId: bookId });
        return this.httpClient.get(url);
    }

    getLastSavedPage(bookId: number): Observable<BookPageMark> {
        var url = this.apiUrlHelper.getUrlForObject('book/getLastSavedPageMark', { bookId: bookId });
        return this.httpClient.get(url);
    }

    openBook(book: Book) {
        if (book.bookUrl) {
            this.readBook(book.id)
                .subscribe(data => {
                    this.router.navigate(['viewBook', book.id, 1, false]);
                },
                err => {
                    this.errorHelper.handleBasicError(err.status, err.errors);
                });
        }
        else {
            this.notifier.notifyAlert("BookDoesNotHaveUploadedContent");
        }
    }

    saveBookPageMark(bookpageMark: BookPageMark): Observable<any> {
        var url = this.apiUrlHelper.getUrl('book/saveBookPageMark');
        return this.httpClient.post(url, bookpageMark);
    }

    getBookPageAnnotation(bookId: number, page: number): Observable<BookAnnotation> {
        var url = this.apiUrlHelper.getUrlForObject('book/getBookPageAnnotationContent', { bookId: bookId, page: page });
        return this.httpClient.get(url);
    }

    saveBookPageAnnotation(bookAnnotation: BookAnnotation): Observable<any> {
        var url = this.apiUrlHelper.getUrl('book/saveBookPageAnnotation');
        return this.httpClient.post(url, bookAnnotation);
    }

    openBookAnnotation(bookpageAnnotation: BookPageAnnotation) {
        this.router.navigate(['viewBook', bookpageAnnotation.book.id, bookpageAnnotation.page, true]);
    }

    getDropdownItemBookCategories(bookCategories: Array<BookCategory>): Array<DropdownItem> {
        var result: Array<DropdownItem> = [];
        bookCategories.forEach(bookCategory => {
            var bookCategoryDropDownItem = new DropdownItem(bookCategory.name, bookCategory.id);
            result.push(bookCategoryDropDownItem);
        });

        return result;
    }


} 