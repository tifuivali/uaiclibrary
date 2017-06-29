import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { User } from '../models/autentification/user';
import { ApiUrlHelper } from '../http-request/http-request';
import { Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpClient } from '../http-client';
import { Account } from '../models/autentification/acount';
import { UserInfo } from '../models/profile/user-info';
import { BookPageAnnotation } from '../models/bookAnnotationPage.model';

@Injectable()
export class UserService {
    constructor(private httpClient: HttpClient,
        private apiUrlHelper: ApiUrlHelper) { }

    addUser(user: User): Observable<User> {
        var url = this.apiUrlHelper.getUrl('user/post');
        return this.httpClient.post(url, user);
    }

    connect(account: Account) {
        var url = this.apiUrlHelper.getUrl('connect');
        return this.httpClient.post(url, account);
    }

    getConnectedUser() {
        var url = this.apiUrlHelper.getUrl('user/getCurrentUser');
        return this.httpClient.get(url);
    }

    getLatestOpenedBooks(count: number) {
        var url = this.apiUrlHelper.getUrlForObject('user/getLatestOpennedBooks', { count: count });
        return this.httpClient.get(url);
    }


    getLatestAnnotedBooks(count: number): Observable<Array<BookPageAnnotation>> {
        var url = this.apiUrlHelper.getUrlForObject('user/getLatestAnnotedBooks', { count: count });
        return this.httpClient.get(url);
    }
} 