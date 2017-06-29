import { Component, OnInit } from '@angular/core';
import { ErorHelper } from '../../../helpers/error.helper';
import { DropdownItem } from '../../common/dropdown/dropdown-item';
import { AuthorService } from '../../../services/author.service';
import { Notifier } from '../../notifications/notifier';
import { Author } from '../../../models/author';
import { ApiUrlHelper } from '../../../http-request/http-request';
import { ModalService } from '../../../services/modal-service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';

@Component({
    selector: 'author-administration',
    templateUrl: './author-administration.component.html'
})

export class AuthorAdministrationComponent implements OnInit {
    constructor(
        private errorHelper: ErorHelper,
        private authorService: AuthorService,
        private notifier: Notifier,
        private apiUrlHlper: ApiUrlHelper,
        private route: ActivatedRoute,
        private modalService: ModalService,
        private router: Router
    ) {
        this.uploadAuthorAvatarUrl = apiUrlHlper.getApiUrl() + 'author/uploadAuthorAvatar';
        this.model.imageUrl = apiUrlHlper.getAuthorImageUrl('default.jpg');
    }

    private model: Author = new Author(0, null, null, null);
    private uploadAuthorAvatarUrl: string;

    initAuthorData() {
        this.authorService.getAuthorById(this.model.id)
            .subscribe(author => {
                this.model = author;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            var authorId = params['authorId'];
            if (authorId) {
                this.model.id = authorId;
                this.initAuthorData();
            }
        }, err => {
            this.notifier.notifyError('AnErrorOcurred');
        })
    }

    onSubmit() {
        this.authorService.saveAuthor(this.model)
            .subscribe(ok => {
                this.notifier.notifySuccess("ModificationWasSaved");
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    updateAuthorImage() {
        this.modalService.open('author-upload-image-modal');
    }

    removeAuthor() {
        if (this.model.id <= 0) return;
        this.authorService.removeAuthor(this.model.id)
            .subscribe(ok => {
                this.notifier.notifySuccess('AuthorWasDeleted');
                this.router.navigate(['/authors']);
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            })
    }

    uploadAvatarImageDone(url: any) {
        this.model.imageUrl = url;
    }

}