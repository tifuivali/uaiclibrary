import { Component } from '@angular/core';
import { TranslateService } from '../../translations';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.material.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    constructor(
        public translateService: TranslateService,
        private authService: AuthService,
        private router: Router
    ) {
        this.authService.setUserBasicInfo();
    }

    public onLogout() {
        this.authService.logOut();
        this.router.navigate(['/login']);
    }
}
