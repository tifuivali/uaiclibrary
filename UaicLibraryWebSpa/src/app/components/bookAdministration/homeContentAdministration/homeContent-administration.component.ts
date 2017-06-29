import { Component, OnInit } from '@angular/core';
import { ErorHelper } from '../../../helpers/error.helper';
import { Notifier } from '../../notifications/notifier';
import { FroalaEditorComponent } from 'ng2-froala-editor/ng2-froala-editor';
import { AdministrationService } from '../../../services/administration.service';
import { Setting } from '../../../models/setting.model';

@Component({
    selector: 'homeContent-administration',
    templateUrl: './homeContent-administration.component.html'
})

export class HomeContentAdministrationComponent implements OnInit {
    constructor(
        private errorHelper: ErorHelper,
        private notifier: Notifier,
        private administrationService: AdministrationService

    ) {
    }

    private content: string;

    froalaOptions: any = {
        height: 300
    };

    editor: any;

    ngOnInit() {
        this.initializeContent();
    }

    initializeContent() {
        this.administrationService.getHomePageContent()
            .subscribe(setting => {
                this.content = setting.value;
            })
    }

    onFroalaModelChanged(event: any) {
        setTimeout(() => {
            this.content = event;
            console.log(this.content);
        }, 100);
    }


    onEditorInitialized(event?: any) {
        this.editor = FroalaEditorComponent.getFroalaInstance();
        this.editor.on('froalaEditor.focus', (e, editor) => {
        });
    }

    changeContent(content: string) {
        this.administrationService.saveHomePageContent(content)
            .subscribe(setting => {
                this.content = setting.value;
                this.notifier.notifySuccess("ContentWasSaved");
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }

    saveContent() {
        this.changeContent(this.content);
    }

    removeContent() {
        this.changeContent(null);
    }

}