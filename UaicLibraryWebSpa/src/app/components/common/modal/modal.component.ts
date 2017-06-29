import { Component, ElementRef, Input, OnInit, OnDestroy, EventEmitter, Output } from '@angular/core';
import { ModalService } from '../../../services/modal-service';
declare var jQuery: any;

@Component({
    selector: 'modal',
    template: '<ng-content></ng-content>',
})

export class ModalComponent implements OnInit, OnDestroy {
    @Input() id: string;
    private element: any;

    @Input()
    public data: any;
    @Input() 
    modalOkId: string = "custom-modal-ok";
    @Input()
    modalCloseId: string = "custom-modal-close";

    @Output() onOpen = new EventEmitter();

    @Output() onClose = new EventEmitter();

    @Output() onDone = new EventEmitter();

    constructor(private modalService: ModalService, private el: ElementRef) {
        this.element = jQuery(el.nativeElement);
    }
    ngOnInit(): void {
        let modal = this;

        // ensure id attribute exists
        if (!this.id) {
            console.error('modal must have an id');
            return;
        }

        // move element to bottom of page (just before </body>) so it can be displayed above everything else
        this.element.appendTo('body');

        // close modal on background click

        this.element.on('click', function (e: any) {
            var target = jQuery(e.target);
            if (!target.closest('.custom-modal-body').length) {
                modal.close();
            }
        });

        var okButton = jQuery('#' + this.modalOkId);
        okButton.on('click', function (e) {
            modal.done(this.data);
        });

        var closeButton = jQuery('#' + this.modalCloseId);
        closeButton.on('click', function (e) {
            modal.close();
        });

        // add self (this modal instance) to the modal service so it's accessible from controllers
        this.modalService.add(this);
    }

    // remove self from modal service when directive is destroyed
    ngOnDestroy(): void {
        this.modalService.remove(this.id);
        this.element.remove();
    }

    // open modal
    open(): void {
        this.element.show();
        jQuery('body').addClass('custom-modal-open');
        this.onOpen.emit();
    }

    done(result): void {
        this.element.hide();
        jQuery('body').removeClass('custom-modal-open');
        this.onDone.emit(result);
    }

    // close modal
    close(): void {
        this.element.hide();
        jQuery('body').removeClass('custom-modal-open');
        this.onClose.emit();
    }
}