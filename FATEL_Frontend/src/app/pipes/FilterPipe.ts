//filter pipe based on query the name
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})

export class FilterPipe implements PipeTransform {
    transform(items: any[], query: string | null): any[] {
      if (!items) return [];
      if (!query) return items;
      query = query.toLowerCase(); //This might be important
      return items.filter(it => {
        return it.name.toLowerCase().includes(query);
      });
    }
}

