import { Component, Input } from '@angular/core';
import { ApiUrlHelper } from '../../../../http-request/http-request';
import { ModalService } from '../../../../services/modal-service';
@Component({
    selector: 'user-profile-header',
    templateUrl: './profile-header.component.html'
})

export class ProfileHeaderComponent {

    @Input()
    public fullName: string;
    @Input()
    public role: string;
    @Input() readedBooks: number;
    @Input() editedBooks: number;
    @Input() openedBooks: number;
    @Input() avatarUrl: string;
    @Input() email: string;
    @Input() faculties: Array<any>;

    constructor(
        private apiUrlHelper: ApiUrlHelper,
        private modalService: ModalService) {
            this.uploadAvararUrl = this.apiUrlHelper.getApiUrl() + 'user/uploadAvatar';
    }

    private uploadAvararUrl: string;

    onClick() {
        this.modalService.open('custom-modal-1');
    }

    uploadAvatarDone(result) {
        this.avatarUrl = result;
    }

    modalDone() {

    }
}
