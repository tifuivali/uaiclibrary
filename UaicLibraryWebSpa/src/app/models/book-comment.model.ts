import { User } from './autentification/user';

export class BookComment {
    constructor(public id: number, 
    public user:User, 
    public text: string, 
    public likes: number, 
    public dislikes: number, 
    public bookId: number,
    public date: Date) { }
}