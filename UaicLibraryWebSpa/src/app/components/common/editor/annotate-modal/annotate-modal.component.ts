import { Component, OnInit, Input, Output, OnChanges, SimpleChanges, EventEmitter } from '@angular/core';
import { FroalaEditorComponent } from 'ng2-froala-editor/ng2-froala-editor';

@Component({
    selector: 'annotate-modal',
    templateUrl: './annotate-modal.component.html',
    styleUrls: ['./annotate-modal.component.css']
})

export class AnnotateModalComponent implements OnChanges, OnInit {

    @Input() modalId: string;
    @Input() content: string = "";
    @Output() onSaveContent = new EventEmitter<string>();


    constructor() {
    }

    editor: any;

    froalaOptions: any = {
        height: 300
    };

    ngOnInit() {
        console.log("OnInitAnnotate");
    }

    ngOnChanges(simpleCganges: SimpleChanges) {
        var newContent = simpleCganges['content'].currentValue;
        console.log(newContent);
        this.content = newContent;
    }

    onFroalaModelChanged(event: any) {
        this.content = event;
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

    modalDone() {
        this.onSaveContent.emit(this.content);
    }
}
