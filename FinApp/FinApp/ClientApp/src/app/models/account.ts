import { Currency } from './currency';
import { Image } from './image';

export class Account {
  id: number;
  name: string;
  currency: Currency;
  image: Image;
  balance: number;

  constructor() {
    this.image = new Image();
    this.currency = new Currency();
  }
}
