import { Component } from '@angular/core';
import { TranslateService } from "../../translations"

@Component({
    selector: 'counter',
    templateUrl: './counter.component.html'
})
export class CounterComponent {
    public currentCount = 0;

    public incrementCounter() {
        this.currentCount++;
    }

    constructor(private _translate: TranslateService) { }
}
 