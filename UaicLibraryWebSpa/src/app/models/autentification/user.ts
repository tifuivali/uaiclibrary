
import { Faculty } from '../../models/faculty';

export class User {
    constructor(
        public Id: number,
        public MatricolNumber: string,
        public Email: string,
        public Password: string,
        public FirstName: string,
        public LastName: string,
        public Role: string,
        public ConfirmPassword: string,
        public Year: number,
        public ImageUrl: string,
        public Faculties: Array<Faculty>) { };
}