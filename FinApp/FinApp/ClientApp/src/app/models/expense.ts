import { Transaction } from './transaction'
import { ExpenseCategory } from './expenseCategory'
import { Account } from './account'


export class Expense {
  id: number;
  expenseCategory: ExpenseCategory;
  account: Account;
  transaction: Transaction;

  constructor() {
    this.expenseCategory = new ExpenseCategory();
    this.transaction = new Transaction();
    this.account = new Account();
  }
}
