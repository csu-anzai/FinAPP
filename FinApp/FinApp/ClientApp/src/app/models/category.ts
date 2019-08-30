import { Image } from './image';

export class Category {
  id: number;
  name: string;
  image: Image;

  constructor() {
    this.image = new Image();
  }
}

