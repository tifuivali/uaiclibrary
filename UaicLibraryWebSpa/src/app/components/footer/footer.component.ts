import { Component, NgModule } from '@angular/core'
import { TranslateService } from "../../translations"
import { Router } from '@angular/router'

@Component({
    selector: 'footer-page',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.css']
})

export class Footer {
    public currentYear: number;
    constructor(
        private _translate: TranslateService,
        private router: Router) {
            this.currentYear = new Date().getFullYear();
    }

}