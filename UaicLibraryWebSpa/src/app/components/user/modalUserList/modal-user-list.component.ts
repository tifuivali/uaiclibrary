import { Component, Input } from '@angular/core';
import { User } from '../../../models/autentification/user';

@Component({
    selector: 'user-modal-list',
    templateUrl: './modal-user-list.component.html'
})

export class UserModalListComponent {
    @Input()
    public users: Array<User>;
    @Input()
    public modalId: string;

    
    constructor() {
    }

}
