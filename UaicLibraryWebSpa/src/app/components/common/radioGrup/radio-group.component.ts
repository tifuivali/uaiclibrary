import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'radio-group',
    templateUrl: './radio-group.component.html'
})

export class RadioGrup {
    @Input()
    public options: any[];
    @Output()
    public onButtonSelected: EventEmitter<string> = new EventEmitter();

    onChange(value) {
        this.onButtonSelected.emit(value);
    }
}