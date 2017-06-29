import {User} from './autentification/user';
import {Book} from './book';

export class BookPageAnnotation {
    constructor(
        public id: number,
        public user: User,
        public book: Book,
        public page: number
    )
    {}
}