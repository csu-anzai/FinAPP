import {Transaction} from './transaction'
import {IncomeCategory} from './incomeCategory'
import {Account} from './account'


export class Incomes
{
    id: number;
    incomeCategory:IncomeCategory;
    account:Account;
    transaction:Transaction;
    
    constructor(){
        this.incomeCategory = new IncomeCategory();
        this.transaction = new Transaction();
        this.account = new Account();
    }
}
