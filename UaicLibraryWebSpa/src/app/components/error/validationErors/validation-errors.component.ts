import { Component, Input, OnInit } from '@angular/core';
import { Error } from '../../../models/error/error';
@Component({
    selector: 'validation-errors',
    templateUrl: './validation-errors.component.html'
})

export class ValidationErrors {
    @Input()
    public errors: Error[];

}