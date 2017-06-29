import { Component, OnInit } from '@angular/core';
import { Student } from '../../../models/student';
import { StudentService } from '../../../services/student.service';
import { ErorHelper } from '../../../helpers/error.helper';
import { ModalService } from '../../../services/modal-service';
import { Notifier } from '../../notifications/notifier';
import { Book } from '../../../models/book';
import { UserService } from '../../../services/user.service';
import { BookService } from '../../../services/book.service';
import { BookPageAnnotation } from '../../../models/bookAnnotationPage.model';

@Component({
    selector: 'user-profile',
    templateUrl: './student-profile.component.html'
})

export class StudentProfileComponent implements OnInit {


    updateLastOpennedBooks() {
        this.userService.getLatestOpenedBooks(5)
            .subscribe(books => {
                this.lastOpennedBooks = books;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

      updateLastAnnotatedBooks() {
        this.userService.getLatestAnnotedBooks(10)
            .subscribe(bookAnnotations => {
                this.lastAnnotatationsBooks = bookAnnotations;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    ngOnInit(): void {
        this.studentService.getCurrentStudent()
            .subscribe(student => {
                this.model = student;
                this.updateLastOpennedBooks();
                this.updateLastAnnotatedBooks();
            },
            err => {
                console.log(err);
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    public onAboutEditClick() {
        this.modalService.open("edit-about-modal");
    }

    public modalDone() {
        this.studentService.updateDescription(this.model.about)
            .subscribe(data => {
                this.notifier.notifySuccess("SuccessUpdated");
            },
            err => {
                console.log(err);
                this.errorHelper.handleBasicError(err.status, err.errors);
            });

    }


    public model: Student;
    private lastOpennedBooks: Array<Book> = [];
    private lastAnnotatationsBooks: Array<BookPageAnnotation> = []

    constructor(
        private studentService: StudentService,
        private errorHelper: ErorHelper,
        private modalService: ModalService,
        private notifier: Notifier,
        private userService: UserService,
        private bookService: BookService
    ) {
        this.model = new Student(0, "", 0, 0, 0, null, "", []);
    }

    viewBook(book: Book) {
        this.bookService.openBook(book);
    }

    viewAnnotation(bookPageAnnotation: BookPageAnnotation) {
        this.bookService.openBookAnnotation(bookPageAnnotation);
    }
}