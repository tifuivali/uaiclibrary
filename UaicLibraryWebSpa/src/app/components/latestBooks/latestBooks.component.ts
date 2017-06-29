import { Component, OnInit } from '@angular/core';
import { TranslateService } from "../../translations"
import { BookService } from '../../services/book.service';
import { Book } from '../../models/book';
import { ErorHelper } from '../../helpers/error.helper';

@Component({
    selector: 'latestBooks',
    templateUrl: './latestBooks.component.html'
})
export class LatestBooksComponent implements OnInit {
    constructor(
        private _translate: TranslateService,
        private bookService: BookService,
        private errorHelper: ErorHelper,
    ) { }

    latestBooks: Array<Book>;

    ngOnInit() {
        this.bookService.getLatest(10)
            .subscribe(books => {
                this.latestBooks = books;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    refreshComponent() {
        this.ngOnInit();
    }


}
