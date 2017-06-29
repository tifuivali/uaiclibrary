import { Component, NgModule, OnInit } from '@angular/core'
import { Languages, LanguagesList } from "../../constants/application-languages"
import { Language } from "../../models/language"
import { TranslateService } from "../../translations"
import { Router } from '@angular/router'
import { AppCookieHelper } from '../../helpers/app-cookie.helper';
import { StringHelper } from '../../helpers/string.helper';

@Component({
    selector: 'language-list',
    templateUrl: './language-list.component.html',
})

export class LanguageListComponent implements OnInit {

    constructor(private _translate: TranslateService,
        private router: Router,
        private stringHelper: StringHelper,
        private appCookieHelper: AppCookieHelper) {

        var currentLang = this.appCookieHelper.getLanguageCode();
        this.selectedLanguage = Languages[currentLang];
    }

    ngOnInit(): void {
        this.languages = LanguagesList;
    }

    selectedLanguage: Language;
    languages: Language[];

    onSelect(language: string) {
        if (this.stringHelper.isNotNullOrEmpty(language)) {
            this.appCookieHelper.setLanguageCode(language);
            this._translate.use(language);
        }
    }
}