import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { ApiUrlHelper } from '../http-request/http-request';
import { Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpClient } from '../http-client';
import { Faculty } from '../models/faculty';
import { MultiSelectItem } from '../components/common/multiselect/multi-select-item';
import { BookAdministrationModel } from '../models/book-administration.model';
import { Author } from '../models/author';
import { Setting } from '../models/setting.model';

@Injectable()
export class AdministrationService {
    constructor(private httpClient: HttpClient,
        private apiUrlHelper: ApiUrlHelper) { }


    saveBook(bookAdministrationModel: BookAdministrationModel): Observable<any> {
        var url = this.apiUrlHelper.getUrl('bookAdministration/saveBook');
        return this.httpClient.post(url, bookAdministrationModel);
    }

    removeBook(bookId: number): Observable<any> {
        var url = this.apiUrlHelper.getUrl('bookAdministration/removeBook');
        return this.httpClient.post(url, bookId);
    }

    getHomePageContent(): Observable<Setting> {
        var url = this.apiUrlHelper.getUrl('setting/getHomePageContent');
        return this.httpClient.get(url);
    }

    saveHomePageContent(content: string): Observable<Setting> {
        var url = this.apiUrlHelper.getUrl('setting/updateHomePageContent');
        return this.httpClient.post(url, content);
    }
} 