import { Component, OnInit, Input } from '@angular/core';
import { BookService } from '../../services/book.service';
import { Book } from '../../models/book';
import { ErorHelper } from '../../helpers/error.helper';
import { AuthorService } from '../../services/author.service';
import { FacultyService } from '../../services/faculty.service';
import { BookData } from '../../data/book.data';
import { BookFilter } from '../../filters/book.filter';
declare var window;
@Component({
    selector: 'book-list',
    templateUrl: './book-list.component.html'
})
export class BookListComponent implements OnInit {
    constructor(
        private bookService: BookService,
        private errorHelper: ErorHelper,
        private authorService: AuthorService,
        private FacultyService: FacultyService
    ) {
        this.bookFilter = new BookFilter(1, 5, [], [], [], null);
    }

    bookData: BookData = new BookData(0, 0, []);
    bookFilter: BookFilter;

    getFilteredBooks() {
        this.bookService.filter(this.bookFilter)
            .subscribe(bookData => {
                this.bookData = bookData;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    getBookCategories() {
        this.bookService.getBookCategories()
            .subscribe(bookCategories => {
                this.bookFilter.categories = bookCategories;
            },
            err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    getBookAuthors() {

        this.authorService.getAuthors()
            .subscribe(authors => {
                this.bookFilter.authors = authors;
            },
            err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            })
    }

    getBookFaculties() {
        this.FacultyService.getAll()
            .subscribe(faculties => {
                this.bookFilter.faculties = this.FacultyService.getSimpleModelsFromFacultyModels(faculties);
            },
            err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            })
    }

    ngOnInit() {
        this.getFilteredBooks();
        this.getBookAuthors();
        this.getBookCategories();
        this.getBookFaculties();
    }

    filter() {
        this.bookFilter.page = 1;
        this.getFilteredBooks();
    }

    onPageChanged(page) {
        console.log('Page Changed to ' + page);
        this.bookFilter.page = page;
        this.getFilteredBooks();
        window.scrollTo(0, 0);
    }

    onPageSizeChanged(pageSize) {
        console.log("Pag size:" + pageSize);
        this.bookFilter.pageSize = pageSize;
        this.getFilteredBooks();
        window.scrollTo(0, 0);
    }

    refreshComponent() {
        this.ngOnInit();
    }

}
