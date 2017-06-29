import { Component, Input, OnInit,OnChanges,SimpleChanges, ElementRef, NgModule, Output, EventEmitter } from '@angular/core';
import { MultiSelectItem } from './multi-select-item';


@Component({
    selector: 'multi-select',
    templateUrl: './multi-select.component.html',
    host: {
        '(document:click)': 'outClick($event)',
    }
})


export class MultiSelect implements OnInit, OnChanges {
    
    
    ngOnInit(): void {
      this.title = this.label;    
    }

    ngOnChanges(changes: SimpleChanges) {
        var newData = changes['data'].currentValue;
        if(newData) {
            this.refreshTitle();
        }
    }


    @Input()
    public data: Array<MultiSelectItem>;

    @Output()
    public dataChange = new EventEmitter<Array<MultiSelectItem>>();

    @Input()
    public label: string;

    private title: string;

    private refreshTitle(): void {
        var selecetdItemsNames: Array<string> = [];
        this.data.forEach(el => {
            if (el.isSelected) {
                selecetdItemsNames.push(el.name);
            }
        });
        if (selecetdItemsNames.length <= 0) {
            this.title = this.label;
        } else {
            this.title = selecetdItemsNames.join();
        }
    }

    constructor(private _eref: ElementRef) {
        this.isActive = false;
        this.data = [
            new MultiSelectItem(1, "Test 1", false),
            new MultiSelectItem(2, "Test 2", true)
        ]
    }

    isActive: boolean;

    open() {
        console.log("Open");
        this.isActive = true;
    }

    onClick(item) {
        item.isSelected = !item.isSelected;
        this.dataChange.emit(this.data);
        this.refreshTitle();

    }

    outClick(event) {
        if (!this._eref.nativeElement.contains(event.target)) {
            this.isActive = false;
        }
    }

}