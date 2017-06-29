import { Injectable, Inject } from '@angular/core';
import { TRANSLATIONS } from './translations'; // import our opaque token
import { StringHelper } from "../helpers/string.helper"
import { appSettings } from "../constants/application-settings"
import { DOCUMENT } from "@angular/platform-browser"


@Injectable()
export class TranslateService {
    private currentLanguage: string;

    // inject our translations
    constructor( @Inject(TRANSLATIONS) private _translations: any,
        private stringHelper: StringHelper) {
    }

    public use(lang: string): void {
        this.currentLanguage = lang;
    }
    private translateArg(key: any) {
        let translation = key;

        if (this._translations[this.currentLanguage] && this._translations[this.currentLanguage][key]) {
            var trans = this._translations[this.currentLanguage][key];;
            return trans;
        }
        return translation;
    }

    private translate(key: string, translateArgs: boolean, args: any[]): string {
        // private perform translation
        let translation = key;
        if (this._translations[this.currentLanguage] && this._translations[this.currentLanguage][key]) {
            var trans = this._translations[this.currentLanguage][key];;
            var translatedArgs = new Array<any>();
            if (args != null || args != undefined) {
                for (var i = 0; i < args.length; i++) {
                    translatedArgs.push(this.translateArg(args[i]));
                }
                if (args.length > 0) {
                    if (translateArgs) {
                        return this.stringHelper.format(trans, translatedArgs);
                    }
                    else {
                        return this.stringHelper.format(trans, args);
                    }
                }
            }
            return trans;
        }
        return translation;
    }

    public instant(key: string, translateArgs?: boolean, args?: any[]) {
        // call translation
        return this.translate(key, translateArgs, args);
    }
}