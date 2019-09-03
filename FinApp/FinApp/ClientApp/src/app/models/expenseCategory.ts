import { Image } from './image';

export class ExpenseCategory{
    id:number;
    name:string;    
    image:Image;
    constructor(){
        this.image = new Image();
    }
}
