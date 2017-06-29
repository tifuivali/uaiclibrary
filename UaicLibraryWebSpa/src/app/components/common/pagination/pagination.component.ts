import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { PaginationData } from '../../../data/pagination.data';
import { ArrayHelper } from '../../../helpers/array.helper';
import { DropdownItem } from '../dropdown/dropdown-item';
@Component({
    selector: 'pagination',
    templateUrl: './pagination.component.html',
    styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit, OnChanges {
    constructor(
        private arrayHelper: ArrayHelper
    ) { }

    private pages: Array<number>;
    private pageSize: number = 5;
    @Input() paginationData: PaginationData;
    @Output() onPageChanged = new EventEmitter<number>();
    @Output() onPageSizeChanged = new EventEmitter<number>();
    @Input() showPageSizes: boolean = true;
    @Input() pageSizes: Array<number> = [5, 10, 50, 100];
    private pageSizeItems: Array<DropdownItem> = []

    modelPage: number;
    private numberOfPagerPages = 5;
    private startPage = 1;
    private endPage = this.numberOfPagerPages;
    onPage(page: number) {
        if (page > this.paginationData.numberOfPages) {
            return;
        }
        this.paginationData.page = page;
        this.modelPage = page;
        this.onPageChanged.emit(page);
    }

    onSetPage(page: number) {
        this.onPage(page);
        this.startPage = page;
        this.endPage = this.startPage + this.numberOfPagerPages;
        if (this.paginationData.numberOfPages < this.endPage) {
            this.endPage = this.paginationData.numberOfPages;
        }
        this.refreshPages(this.paginationData.numberOfPages)
    }

    ngOnInit() {
        for (var i = 0; i < this.pageSizes.length; i++) {
            var pageSize = this.pageSizes[i];
            this.pageSizeItems.push(new DropdownItem(pageSize.toString(), pageSize));
        }
        if (this.pages) {
            return;
        }
        this.refreshPages(this.paginationData.numberOfPages);
    }

    refreshPages(numberOfPages: number) {
        if (numberOfPages < this.endPage) {
            this.endPage = numberOfPages;
        }
        if (this.endPage == 0) {
            this.endPage = numberOfPages;
        }
        this.pages = this.arrayHelper.getRangeNumber(this.startPage, this.endPage);
    }

    ngOnChanges(changes: SimpleChanges) {
        var newPagData = changes['paginationData'].currentValue;
        // var newPageSizes = changes['pageSizes'].currentValue;
        // for(var i=0;i<newPageSizes.length;i++)
        // {
        //     var pageSize = newPageSizes[i];
        //     this.pageSizeItems.push(new DropdownItem(pageSize,pageSize));
        // }
        this.refreshPages(newPagData.numberOfPages);
    }

    back() {
        if (this.startPage > 1) {
            this.endPage = this.startPage;
            this.startPage -= this.numberOfPagerPages;
        }
        if (this.startPage <= 0) {
            this.startPage = 1;
        }

        this.onPage(this.startPage);
        this.refreshPages(this.paginationData.numberOfPages);
    }

    next() {
        if (this.endPage < this.paginationData.numberOfPages) {
            this.startPage = this.endPage;
            this.endPage += this.numberOfPagerPages;
        }
        if (this.endPage > this.paginationData.numberOfPages) {
            this.endPage = this.paginationData.numberOfPages;
        }
        this.onPage(this.startPage);
        this.refreshPages(this.paginationData.numberOfPages);
    }

    setPageSize(pageSize: DropdownItem) {
        this.pageSize = pageSize.value;
        this.onPageSizeChanged.emit(pageSize.value);
    }
}
