import { Currency } from './currency';
import { Image } from './image';
import { Incomes } from './incomes';


export class Account {
  id: number;
  name: string;
  currency: Currency;
  image: Image;
  balance: number;
  incomes : Incomes;

  constructor() {
    this.image = new Image();
    this.currency = new Currency();
  }
}
