import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'ifnull'
})
export class IfnullPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }

}
