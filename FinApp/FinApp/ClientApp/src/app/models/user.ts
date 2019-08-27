import { Account } from './account'

export class User {
    id: number;
    name: string;
    surname: string;
    email: string;
    password: string;
    birthDate: Date;
    avatar: string;
    roleId: number;
    accounts: Account[];
}