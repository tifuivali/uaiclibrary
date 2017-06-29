import { PaginationFilter } from './pagination.filter';
import { MultiSelectItem } from '../components/common/multiselect/multi-select-item';

export class BookFilter extends PaginationFilter {
    constructor(public page: number, public pageSize: number, public categories: Array<MultiSelectItem>,
        public authors: Array<MultiSelectItem>, public faculties: Array<MultiSelectItem>, public title: string) {
        super(page, pageSize);
    }
}