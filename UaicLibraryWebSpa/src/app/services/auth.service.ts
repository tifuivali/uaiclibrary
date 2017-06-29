import { Injectable } from '@angular/core';
import { AppCookieHelper } from '../helpers/app-cookie.helper';
import { UserService } from '../services/user.service';

@Injectable()
export class AuthService {
    public userIsLogged: boolean = false;
    public userRole: string;
    public userId: number;
    public userEmail: string;
    public userIsAdmin: boolean = false;

    constructor(private cookieHelper: AppCookieHelper, private userService: UserService) {

        this.userIsLogged = this.cookieHelper.userIsAuthenticated();
    }

    private changeAuthStatus(status: boolean): void {
        this.userIsLogged = status;
        this.userIsAdmin = false;
    }

    public logIn(token: any) {
        this.cookieHelper.setAuthToken(token);
        this.changeAuthStatus(true);
        this.setUserBasicInfo();
    }

    public setUserBasicInfo() {
        this.userService.getConnectedUser()
            .subscribe(res => {
                this.userEmail = res.value.email;
                this.userId = res.value.id;
                this.userRole = res.value.role;
                this.userIsAdmin = res.value.role == "Admin";
            },
            err => {

            });
    }

    public logOut() {
        this.cookieHelper.setAuthToken(null);
        this.changeAuthStatus(false);
    }
}