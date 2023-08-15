import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'ifempty'
})
export class IfemptyPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    if(value == null || value == "" || value == undefined || Number.isNaN(value) || value == "NaN"){
      return "------";
    }else {
      return value;
    }
  }

}
