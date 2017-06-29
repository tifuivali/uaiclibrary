import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ErorHelper } from '../../helpers/error.helper';
import { Comment } from '../../models/comment.model';
import { Book } from '../../models/book';
import { BookCommentService } from '../../services/book.comment.service';
import { BookComment } from '../../models/book-comment.model';
import { User } from '../../models/autentification/user';
import { AuthService } from '../../services/auth.service';

@Component({
    selector: 'book-comments',
    templateUrl: './book-comment-list.component.html'
})
export class BookCommentListComponent implements OnInit, OnChanges {
    constructor(
        private errorHelper: ErorHelper,
        private bookCommentService: BookCommentService,
        private authService: AuthService
    ) {

    }

    @Input() enable: boolean;
    @Input() book: Book;
    bookComments: Array<Comment>;
    currentUserId: number;

    refreshComments() {
        this.bookCommentService.getComments(this.book.id)
            .subscribe(comments => {
                this.bookComments = comments;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }



    ngOnInit() {
        this.currentUserId = this.authService.userId;
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes['enable'].currentValue) {
            this.refreshComments();
        }
    }

    onAddComment(comment: Comment) {
        var bookComment = new BookComment(0, comment.user, comment.text, comment.likes, comment.dislikes, this.book.id, null);
        this.bookCommentService.addBookComment(bookComment)
            .subscribe(data => {
                this.refreshComments();
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    removeComment(comment: Comment) {
        this.bookCommentService.removeBookComment(comment.id)
            .subscribe(data => {
                this.refreshComments();
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    onCommentLike(comment: Comment) {
        this.bookCommentService.likeComment(comment.id)
            .subscribe(data => {

            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    onCommentDislike(comment: Comment) {
        this.bookCommentService.dislikeComment(comment.id)
            .subscribe(data => {

            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }
}
