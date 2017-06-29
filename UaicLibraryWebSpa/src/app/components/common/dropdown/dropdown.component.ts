import { Component, OnInit, Input, Output,ElementRef, EventEmitter } from '@angular/core';
import { DomSanitizer, SafeStyle } from '@angular/platform-browser';
import { DropdownItem } from './dropdown-item';

@Component({
    selector: 'dropdown',
    templateUrl: './dropdown.component.html',
    host: {
        '(document:click)': 'outClick($event)',
    }
})
export class DropdownComponent {
    constructor(
        private _eref: ElementRef,
        private domSanitizer: DomSanitizer
    ) {
        this.label = "Dropdown";
        this.isActive = false;
        this.activeStyle = domSanitizer.bypassSecurityTrustStyle('width: 180px; position: absolute; opacity: 1; display: block;');
        this.inactiveStyle = domSanitizer.bypassSecurityTrustStyle('width: 132px; position: absolute; opacity: 1; display: none;');
        this.activeItemStyle = domSanitizer.bypassSecurityTrustStyle('background-color: bisque');
        this.inactiveItemStyle = domSanitizer.bypassSecurityTrustStyle('');
    }

    @Input() label: string;
    @Input() isActive: boolean;

    private activeStyle: SafeStyle;
    private inactiveStyle: SafeStyle;
    private activeItemStyle: SafeStyle;
    private inactiveItemStyle: SafeStyle;

    @Input() items: Array<DropdownItem> = [];
    @Input() selected: DropdownItem;

    @Output() onChange = new EventEmitter<any>();

    onClick() {
        this.isActive = !this.isActive;
    }

    outClick(event) {
        if (!this._eref.nativeElement.contains(event.target)) {
            this.isActive = false;
        }
    }

    onSelect(item: DropdownItem) {
        this.label = item.text;
        this.onChange.emit(item);
        this.isActive = false;
        this.selected = item;
    }

    isCurrentItemSelected(item: DropdownItem) {
        if(this.selected) {
            return this.selected.value == item.value;
        }

        return false;
    }
}
