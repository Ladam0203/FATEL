import { PipeTransform, Pipe } from '@angular/core';

@Pipe({name: 'keys'})
export class KeysPipe implements PipeTransform {
  transform(value: any) : any {
    let keys = [];
    for (let key in value) {
      if (isNaN(parseInt(key))) keys.push(key)
    }
    return keys;
  }
}
