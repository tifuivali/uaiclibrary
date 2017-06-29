import { Component, OnInit } from '@angular/core';
import { TranslateService } from "../../translations"
import { ErorHelper } from '../../helpers/error.helper';
import { AdministrationService } from '../../services/administration.service';
import { Setting } from '../../models/setting.model';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
    constructor(
        private _translate: TranslateService,
        private errorHelper: ErorHelper,
        private administrationService: AdministrationService
    ) {

    }

    content: string;

    ngOnInit() {
        this.administrationService.getHomePageContent()
            .subscribe(setting => {
                this.content = setting.value;
            }, err => {
                this.errorHelper.handleBasicError(err.status, err.errors);
            });
    }


}
