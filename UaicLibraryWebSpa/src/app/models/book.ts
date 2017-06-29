import { Author } from './author';
import { BookComment } from './book-comment.model';
import { BookCategory } from './book-category';
import { Faculty } from './faculty';

export class Book {
    constructor(
        public id: number,
        public isbn: string,
        public bookAuthors: Array<Author>,
        public description: string,
        public name: string,
        public numberOfPages: number,
        public comments: Array<BookComment>,
        public bookCategory: BookCategory,
        public publishDate: Date,
        public likes: number,
        public dislikes: number,
        public faculty: Faculty,
        public bookUrl: string,
        public views: number,
        public reads: number,
        public imageUrl: string,
        public pageAnnotations: number
    ) { }
}