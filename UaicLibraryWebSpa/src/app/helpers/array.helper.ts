import { Injectable } from '@angular/core';
import { MultiSelectItem } from '../components/common/multiselect/multi-select-item';

@Injectable()
export class ArrayHelper {
    constructor() { }

    getRangeNumber(from: number, to: number): Array<number> {
        var result: Array<number> = [];
        for (var i = from; i <= to; i++) {
            result.push(i);
        }

        return result;
    }

    getRangeNumberGratherTo(from: number, to: number, orTo: number): Array<number> {
        var result: Array<number> = [];
        var end = to < orTo ? to : orTo;
        for (var i = from; i <= end; i++) {
            result.push(i);
        }

        return result;
    }

    getSelectedFromList2MultiSelectItems(list1: Array<MultiSelectItem>, list2: Array<MultiSelectItem>): Array<MultiSelectItem> {
        var result: Array<MultiSelectItem> = [];
        list1.forEach(elemnt1 => {
            var ok: boolean = true;
            list2.forEach(elemnt2 => {
                if (elemnt1.id == elemnt2.id) {
                    ok = false;
                    result.push(elemnt2);
                }
            })
            if (ok) {
                elemnt1.isSelected = false;
                result.push(elemnt1);
            }
        })

        return result;
    }
}