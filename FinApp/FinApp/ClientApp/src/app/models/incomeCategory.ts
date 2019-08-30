import { Image } from './image';

export class IncomeCategory{
    id:number;
    name:string;    
    image:Image;
    constructor(){
        this.image = new Image();
    }
}