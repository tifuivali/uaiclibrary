import { Faculty } from '../models/faculty';
export class Student {
    public id: number;
    public fullName: string;
    public role: string;
    public readedBooks: number;
    public editedBooks: number;
    public opennedBooks: number;
    public about: string;
    public imageUrl: string;
    public faculties: Array<Faculty>;
    constructor(id: number, fullName: string, readedBooks: number, editedBooks: number, opennedBooks: number, imageUrl: string,
        about: string, faculties: Array<Faculty>) {
        this.id = id;
        this.fullName = fullName;
        this.readedBooks = readedBooks;
        this.editedBooks = editedBooks;
        this.opennedBooks = opennedBooks;
        this.about = about;
        this.imageUrl = imageUrl;
        this.faculties = faculties;
    }
}