import { Notifier } from '../components/notifications/notifier';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable()

export class ErorHelper {
    public constructor(private router: Router, private notifier: Notifier) { }

    public handleBasicError(statusCode: number, errors: any) {
        
        if (statusCode == 401) {
            this.notifier.notifyAlert('NotAuthenticated');
            this.router.navigate(['/login']);
            return;
        } else
            if (statusCode == 400) {
                this.notifier.notifyErrors(errors);
            } else {
                this.notifier.notifyError('OperationError');
            }
    }
}