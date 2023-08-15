import { Pipe, PipeTransform } from '@angular/core';
import { differenceInYears } from 'date-fns';

@Pipe({
  name: 'dobToAge'
})
export class DobToAgePipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    if(value != undefined || value != null){
      return differenceInYears(new Date(), new Date(value as string))
    }else {
      return "";
    }
  }

}
