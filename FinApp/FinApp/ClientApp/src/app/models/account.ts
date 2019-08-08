import { Currency } from './currency';

export class Account {
  Id: number;
  Name: string;
  Currency: Currency;
  Image: string;
  Balance: number;

  constructor(id: number,
    name: string,
    currency: Currency,
    image: string,
    balance: number) {
    this.Id = id;
    this.Name = name;
    this.Currency = currency;
    this.Image = image;
    this.Balance = balance;
  }
}
