import { Component } from '@angular/core'
import { TranslateService } from "../../translations"

@Component({
    selector: 'page-not-found',
    templateUrl: './page-not-found.component.html',
})

export class PageNotFound {
    constructor(private translateService: TranslateService) { }
}
