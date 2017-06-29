import { Injectable } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { TranslateService } from '../../translations';
import { Error } from '../../models/error/error';

@Injectable()

export class Notifier {
    public constructor(private notifier: NotificationsService, private translator: TranslateService) { }

    public notifyErrors(errors: Error[]) {
        console.log("Notify errr");
        for (var i = 0; i < errors.length; i++) {
            var error = errors[i];
            var title = this.translator.instant('Error');
            var message = this.translator.instant(error.errorMessage, true, error.parameters);
            this.notifier.error(title, message);
        }
    }

    public notifySuccess(message?: string, args?:any[]) {
        var title = this.translator.instant("Success");
        var messageToShow = this.translator.instant(message, true, args);
        this.notifier.success(title, messageToShow);
    }

    public notifyError(error: string, args?:any[])
    {
        var title = this.translator.instant("Error");
        var messageToShow = this.translator.instant(error, true, args);
        this.notifier.error(title,messageToShow);
    }

    public notifyAlert(alert: string, args?:any[])
    {
        var title = this.translator.instant("Warning");
        var messageToShow = this.translator.instant(alert, true, args);
        this.notifier.alert(title,messageToShow);
    }
}