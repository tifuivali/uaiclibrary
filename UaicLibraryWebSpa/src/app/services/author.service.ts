import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { ApiUrlHelper } from '../http-request/http-request';
import { Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpClient } from '../http-client';
import { MultiSelectItem } from '../components/common/multiselect/multi-select-item';
import { Author } from '../models/author';

@Injectable()
export class AuthorService {
    constructor(private httpClient: HttpClient,
        private apiUrlHelper: ApiUrlHelper) { }

    getAuthors(): Observable<Array<MultiSelectItem>> {
        var url = this.apiUrlHelper.getUrl('author/getAuthors');
        return this.httpClient.get(url);
    }

    getMultiSelectItemsFromAuthors(authors: Array<Author>): Array<MultiSelectItem> {
        var result:Array<MultiSelectItem> = []
        authors.forEach(author => {
            var authorMultiSelectItem = new MultiSelectItem(author.id, author.name, true);
            result.push(authorMultiSelectItem);
        });

        return result;
    }

    getAuthorById(authorId: number): Observable<Author> {
        var url = this.apiUrlHelper.getUrlForObject('author/getAuthor', {authorId: authorId});
        return this.httpClient.get(url);
    }

    
    saveAuthor(author: Author): Observable<any> {
        var url = this.apiUrlHelper.getUrl('author/saveAuthor');
        return this.httpClient.post(url, author);
    }

    removeAuthor(authorId: number): Observable<any> {
        var url = this.apiUrlHelper.getUrl('author/removeAuthor');
        return this.httpClient.post(url, authorId);
    }
} 