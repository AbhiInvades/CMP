import { Pipe, PipeTransform } from '@angular/core';
import { isString } from 'util';
import { StoreService } from 'src/app/Services/Store/StoreService';

@Pipe({
  name: 'console'
})
export class ConsolePipe implements PipeTransform {

  constructor(private store :StoreService){}

  transform(value: number, ...args: unknown[]): any {
    console.log(value)
    if(this.store.images){
    return this.store.images.get(value);
    }

  }

}
