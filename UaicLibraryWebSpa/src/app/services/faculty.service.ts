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
import { DropdownItem } from '../components/common/dropdown/dropdown-item';

@Injectable()
export class FacultyService {
    constructor(private httpClient: HttpClient,
        private apiUrlHelper: ApiUrlHelper) { }

    getAll(): Observable<Array<Faculty>> {
        var url = this.apiUrlHelper.getUrl('faculty/getAll');
        return this.httpClient.get(url);
    }

    getSimpleModelsFromFacultyModels(faculties: Array<Faculty>): Array<MultiSelectItem> {
        var result: Array<MultiSelectItem> = [];
        faculties.forEach(element => {
            var simpleModel = new MultiSelectItem(element.id, element.name, false);
            result.push(simpleModel);
        });

        return result;
    }

    getDropDownItemsFromFaculties(faculties: Array<Faculty>): Array<DropdownItem> {
        var result: Array<DropdownItem> = []
        faculties.forEach(faculty => {
            var facultyDropdownItem = new DropdownItem(faculty.name, faculty.id);
            result.push(facultyDropdownItem);
        })

        return result;
    }

    getFacultyById(facultyId: number): Observable<Faculty> {
        var url = this.apiUrlHelper.getUrlForObject('faculty/getFaculty', { facultyId: facultyId });
        return this.httpClient.get(url);
    }

    saveFaculty(faculty: Faculty): Observable<Faculty> {
        var url = this.apiUrlHelper.getUrl('faculty/saveFaculty');
        return this.httpClient.post(url, faculty);
    }

    removeFaculty(facultyId: number): Observable<any> {
        var url = this.apiUrlHelper.getUrl('faculty/removeFaculty');
        return this.httpClient.post(url, facultyId);
    }
} 