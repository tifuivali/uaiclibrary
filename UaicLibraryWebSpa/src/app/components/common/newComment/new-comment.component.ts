import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User } from '../../../models/autentification/user';
import { Comment } from '../../../models/comment.model';
import { UserService } from '../../../services/user.service';
import { ErorHelper } from '../../../helpers/error.helper';

@Component({
    selector: 'new-comment',
    templateUrl: './new-comment.component.html'
})


export class NewCommentComponent implements OnInit {
    constructor(
        private userService: UserService,
        private errorHelper: ErorHelper
    ) {
    }

    comment: Comment = new Comment(0, null, "", 0, 0, null);
    @Output() onAddComment = new EventEmitter<Comment>();

    ngOnInit() {
        this.userService.getConnectedUser()
            .subscribe(res => {
                this.comment.user = res.value;
                console.log(this.comment);
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    addComment() {
        this.onAddComment.emit(this.comment);
        this.comment.text = "";
    }

}
