import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { Student } from '../models/student';
import { ApiUrlHelper } from '../http-request/http-request';
import { Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpClient } from '../http-client';

@Injectable()
export class StudentService {
    constructor(private httpClient: HttpClient,
        private apiUrlHelper: ApiUrlHelper) { }

    getCurrentStudent(): Observable<Student> {
        var url = this.apiUrlHelper.getUrl('student/get');
        return this.httpClient.get(url);
    }

    updateDescription(description: string) {
        var url = this.apiUrlHelper.getUrl('student/updateDescription');
        return this.httpClient.put(url, description);
    }
} 