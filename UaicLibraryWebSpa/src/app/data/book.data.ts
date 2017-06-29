import { PaginationData } from './pagination.data';
import { Book } from '../models/book';

export class BookData extends PaginationData {
    constructor(public page: number, public numberOfPages: number,public books:Array<Book>) {
        super(page, numberOfPages);
    }
}