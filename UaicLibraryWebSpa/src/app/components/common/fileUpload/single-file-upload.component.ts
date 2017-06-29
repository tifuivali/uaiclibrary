import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FileUploader, FileItem, ParsedResponseHeaders } from 'ng2-file-upload';
import { AppCookieHelper } from '../../../helpers/app-cookie.helper';
import { ErorHelper } from '../../../helpers/error.helper';
import { Notifier } from '../../notifications/notifier';

@Component({
    selector: 'single-file-uploader',
    templateUrl: './single-file-upload.component.html'
})
export class SingleFileUploader implements OnInit {
    @Input()
    private url: string;

    @Output() onComplette = new EventEmitter();
    public uploader: FileUploader;

    constructor(private cookieHelper: AppCookieHelper, private errorHelper: ErorHelper, private notifier: Notifier) {
    }

    ngOnInit(): void {
        console.log("Single uplopader file init URL:" +this.url);
        var token = 'Bearer ' + this.cookieHelper.getAuthToken();
        this.uploader = new FileUploader({ url: this.url, authToken: token, method: 'POST' });
        this.uploader.onProgressItem = (fileItem: FileItem, progress: any) => {
            console.log("Progress " + progress);
        }

        this.uploader.onErrorItem = (item: FileItem, response: string, status: number, headers: ParsedResponseHeaders) => {
            try {
                var errors = JSON.parse(response);
            } catch (e) {
                this.notifier.notifyError("ServerErrorOcurred");
                this.uploader.clearQueue();
            }
            this.errorHelper.handleBasicError(errors.status, errors.errors);
            this.uploader.clearQueue();
        }

        this.uploader.onCompleteItem = (item: FileItem, response: string, status: number, headers: ParsedResponseHeaders) => {
            var result = response;
            console.log("On Complette emit");
            this.onComplette.emit(result);
            this.uploader.clearQueue();
            this.notifier.notifySuccess("SuccessUploaded");
        }
    }

}