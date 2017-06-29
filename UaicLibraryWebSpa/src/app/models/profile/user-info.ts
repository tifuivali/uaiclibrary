export class UserInfo {
    public id: number;
    public fullName:string;
    public role: string;
    public readedBooks: number;
    public publishedBooks: number;
    public opennedBooks: number;
    public about: string
    constructor(id:number, fullName:string, role: string, readedBooks: number, publishedBooks: number, opennedBooks: number, about: string) {
        this.id = id;
        this.fullName = fullName;
        this.role = role;
        this.readedBooks = readedBooks;
        this.publishedBooks = publishedBooks;
        this.opennedBooks = opennedBooks;
        this.about = about;
     }
}