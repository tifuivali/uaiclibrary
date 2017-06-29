import { appSettings } from '../constants/application-settings';
import { Headers } from '@angular/http';

declare var $;

export class ApiUrlHelper {
    /**
     * @param apiUrl Url adress.
     * @param args Arguments.
     */
    public getUrl(apiUrl: string, args?: any[]) {
        if (args == null || args == undefined) {
            return appSettings.serviceApiUrl + apiUrl;
        }

        var url = appSettings.serviceApiUrl + apiUrl + '?';
        for (var i = 0; i < args.length; i++) {
            url += args[i][0] + '=' + args[i][1] + '&';
        }

        return url;

    }

    public getDefaultvatarImageUrl() {
        return appSettings.baseApiContentUrl + "images/avatars/default.jpg";
    }

    public getUrlForObject(apiUrl: string, arg: Object) {
        if (arg == null || arg == undefined) {
            return appSettings.serviceApiUrl + apiUrl
        }
        var url = appSettings.serviceApiUrl + apiUrl + '?';
        for (var key in arg) {
            url += key + '=' + arg[key] + '&';
        }
        return url;
    }

    encodeQueryData(apiUrl: string, data) {
        let ret = [];
        for (let d in data)
            ret.push(encodeURIComponent(d) + '=' + encodeURIComponent(data[d]));
        var uri = appSettings.serviceApiUrl + apiUrl + '?' + ret.join('&');
        return uri;
    }

    public getJsonHeaders() {
        var headers = new Headers({ 'Content-Type': 'application/json' });
        return headers;
    }

    public getBookImageUrl(imageNameWithExtension: string): string {
        return appSettings.imagesUrl + 'books/' + imageNameWithExtension;
    }

    public getAuthorImageUrl(imageNameWithExtension: string): string {
         return appSettings.imagesUrl + 'authors/' + imageNameWithExtension;
    }

    public getApiUrl(): string {
        return appSettings.serviceApiUrl;
    }
}