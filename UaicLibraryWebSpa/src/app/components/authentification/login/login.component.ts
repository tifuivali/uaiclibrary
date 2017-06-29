import { Component, NgModule } from '@angular/core'
import { TranslateService } from "../../../translations"
import { Account } from '../../../models/autentification/acount';
import { UserService } from '../../../services/user.service';
import { ErorHelper } from '../../../helpers/error.helper';
import { Router } from '@angular/router';
import { Notifier } from '../../notifications/notifier';
import { AuthService } from '../../../services/auth.service';
@Component({
    selector: 'user-login',
    templateUrl: './login.component.html'
})

export class Login {

    constructor(
        private translateService: TranslateService,
        private userService: UserService,
        private errorHelper: ErorHelper,
        private authService: AuthService,
        private router: Router,
        private notifier: Notifier) { }

    model = new Account("tifuivali@gmail.com", "123456");
    submitted = false;
    onSubmit() {
        this.submitted = true;
        this.userService.connect(this.model)
            .subscribe(jwt => {
                this.authService.logIn(jwt.access_token);
                this.notifier.notifySuccess("LogInSuccessfuly");
                this.router.navigate(['/']);
            },
            err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });

    }
}

