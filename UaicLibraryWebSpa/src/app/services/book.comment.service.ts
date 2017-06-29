import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { ApiUrlHelper } from '../http-request/http-request';
import { Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpClient } from '../http-client';
import { Comment } from '../models/comment.model';
import { BookComment } from '../models/book-comment.model';

@Injectable()
export class BookCommentService {
    constructor(private httpClient: HttpClient,
        private apiUrlHelper: ApiUrlHelper) { }

    getComments(bookId: number): Observable<Array<Comment>> {
        var url = this.apiUrlHelper.getUrlForObject('bookcomments/getBookComments', { bookId: bookId });
        return this.httpClient.get(url);
    }

    addBookComment(bookComment: BookComment): Observable<any> {
        var url = this.apiUrlHelper.getUrl("bookcomments/addBookComment");
        return this.httpClient.post(url, bookComment);
    }

    removeBookComment(commentId: number): Observable<any> {
        var url = this.apiUrlHelper.getUrl("bookcomments/removeComment");
        return this.httpClient.post(url, commentId);
    }

    likeComment(commentId: number): Observable<any> {
        var url = this.apiUrlHelper.getUrl("bookcomments/likeComment");
        return this.httpClient.post(url, commentId);
    } 

    dislikeComment(commentId: number): Observable<any> {
        var url = this.apiUrlHelper.getUrl("bookcomments/dislikeComment");
        return this.httpClient.post(url, commentId);
    } 

} 