import { User } from './autentification/user';

export class Comment {
    constructor(public id: number, 
    public user:User, 
    public text: string, 
    public likes: number, 
    public dislikes: number,
    public date: Date) { }
}