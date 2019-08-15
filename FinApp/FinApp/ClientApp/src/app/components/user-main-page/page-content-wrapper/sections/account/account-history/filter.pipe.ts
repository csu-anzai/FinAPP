import { Pipe, PipeTransform } from '@angular/core'

@Pipe({
  name: 'filterPipe',
  pure: false
})
export class FilterPipe implements PipeTransform {
  transform(value: any, key: string): any {
    console.log(value);
    console.log(key);
    
    return value.filter((item) => item.Description.indexOf(key) !== -1);
  }
}
