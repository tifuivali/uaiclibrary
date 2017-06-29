import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Comment } from '../../models/comment.model';

@Component({
    selector: 'comment',
    templateUrl: './comment.component.html'
})
export class CommentComponent {
    constructor(
    ) { }

    @Input() comment: Comment;
    @Output() onRemove = new EventEmitter<Comment>();
    @Output() onLike = new EventEmitter<Comment>();
    @Output() onDislike = new EventEmitter<Comment>();
    @Input() enableRemove: boolean = true;

    likeIsEnabled = true;

    remove() {
        this.onRemove.emit(this.comment);
    }

    like() {
        if(!this.likeIsEnabled)
        {
            return;
        }
        this.onLike.emit(this.comment);
        this.comment.likes++;
        this.likeIsEnabled = false;
    }

    dislike() {
        if(!this.likeIsEnabled)
        {
            return;
        }
        this.onDislike.emit(this.comment);
        this.comment.dislikes++;
        this.likeIsEnabled = false;
    }

}
