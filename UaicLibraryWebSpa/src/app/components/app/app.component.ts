import { Component, OnInit } from '@angular/core';
import { TranslateService } from "../../translations"
import { appSettings } from '../../constants/application-settings'
import { StringHelper } from '../../helpers/string.helper';
import { AppCookieHelper } from '../../helpers/app-cookie.helper';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    constructor(private _translate: TranslateService,
        private stringHelper: StringHelper,
        private appCookieHelper: AppCookieHelper) { }

    public options = {
        position: ["top", "right"],
        timeOut: 3000,
        lastOnBottom: true
    }

    ngOnInit() {
        this.selectLang();
    }

    selectLang() {
        // set current lang;
        var lang = this.appCookieHelper.getLanguageCode();
        this._translate.use(lang);
    }
}
