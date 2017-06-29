export class BookAnnotation {
    constructor(
        public bookId: number,
        public content: string,
        public page: number,
        public date: Date
    )
    {}
}