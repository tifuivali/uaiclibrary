import { Injectable } from '@angular/core';
import { CookieService } from 'angular2-cookie/core';
import { appSettings } from '../constants/application-settings'
import { StringHelper } from '../helpers/string.helper';

@Injectable()
export class AppCookieHelper {
    constructor(private cookieService: CookieService, private stringHelper: StringHelper) { }

    public getLanguageCode(): string {
        var langCode = this.cookieService.get(appSettings.userLanguageCookieKeyName);
        if (this.stringHelper.isNullOrEmpty(langCode)) {
            return appSettings.defaultLang;
        }
        return langCode;
    }

    public setLanguageCode(lang: string) {
        if(this.stringHelper.isNotNullOrEmpty(lang)){
            this.cookieService.put(appSettings.userLanguageCookieKeyName, lang);
        }
    }

    public setAuthToken(token: string)
    {
        this.cookieService.put(appSettings.authentificationBearerCookieKey, token);
    }

    public getAuthToken(): string
    {
        return this.cookieService.get(appSettings.authentificationBearerCookieKey);
    }

    public userIsAuthenticated(): boolean {
        var token = this.getAuthToken();
        if(this.stringHelper.isNullOrEmpty(token))
            return false;
        return true;
    }
}