import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { Headers, RequestOptions, RequestMethod } from '@angular/http';
import { AppCookieHelper } from './helpers/app-cookie.helper'

@Injectable()

export class HttpClient {

    public constructor(private http: Http, private cookieHelper: AppCookieHelper) {
        
    }

    private createRequestHeaders(cookieHelper): Headers {
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Authorization', 'Bearer ' + cookieHelper.getAuthToken());
        return headers;
    }

    public post(url: string, body: any): Observable<any> {
        var requestHeaders = this.createRequestHeaders(this.cookieHelper);
        return this.http.post(url, JSON.stringify(body), { headers: requestHeaders })
            .map((res: Response) => {
                if(res.text().length > 0) {
                return res.json()
                }
            })
            .catch((error: any) => {
                var errors;
                if(error.status < 401)
                {
                    errors = error.json();
                }
                return Observable.throw({ 'status': error.status, 'errors': errors });
            });
    }

    public put(url: string, body: any): Observable<any> {
        var requestHeaders = this.createRequestHeaders(this.cookieHelper);
        return this.http.put(url, JSON.stringify(body), { headers: requestHeaders })
            .map((res: Response) => {
                if(res.text().length > 0) {
                return res.json()
                }
            })
            .catch((error: any) => {
                var errors;
                if(error.status < 401)
                {
                    errors = error.json();
                }
                return Observable.throw({ 'status': error.status, 'errors': errors });
            });
    }
    
    public get(url: string): Observable<any> {
        var requestHeaders = this.createRequestHeaders(this.cookieHelper);
        return this.http.get(url, {headers: requestHeaders})
            .map((res: Response) => {
                if(res.text().length > 0) {
                return res.json()
            }
            })
            .catch((error: any) => {
                var errors;;
                if(error.status < 401)
                {
                    errors = error.json();
                }
                return Observable.throw({ 'status': error.status, 'errors': errors });
            });
    }
}