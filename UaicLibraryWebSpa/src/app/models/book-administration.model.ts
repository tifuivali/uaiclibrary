import { MultiSelectItem } from '../components/common/multiselect/multi-select-item';
import { BookCategory } from './book-category';

export class BookAdministrationModel {
    constructor(
        public id: number,
        public isbn: string,
        public bookAuthors: Array<MultiSelectItem>,
        public description: string,
        public name: string,
        public numberOfPages: number,
        public bookCategoryId: number,
        public publishDate: Date,
        public facultyId: number,
        public bookUrl: string,
        public imageUrl: string,
    ) { }
}