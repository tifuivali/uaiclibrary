import { Injectable } from '@angular/core'

@Injectable()
export class StringHelper {
    public isNullOrEmpty(text: string): boolean {
        if (text == null || text == undefined || text.length <= 0)
            return true;
        return false;
    }

    public isNotNullOrEmpty(text: string): boolean {
        return !this.isNullOrEmpty(text);
    }

    public format(text: string, args: any[]) {
        console.log("forFormat:"+text);
        console.log('args:');
        console.log(args);

        var result = text.replace(/\{(\d+)\}/g, function (match, capture) {
            console.log("Match:"+match);
            console.log("Capture:"+capture);
            return args[capture];
        });

        console.log("result:"+result);
        return result;
    }
}