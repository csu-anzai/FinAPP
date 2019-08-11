import { Currency } from './currency';
import { Image } from './image';

export class Account {
  Id: number;
  Name: string;
  Currency: Currency;
  Image: Image;
  Balance: number;
}
