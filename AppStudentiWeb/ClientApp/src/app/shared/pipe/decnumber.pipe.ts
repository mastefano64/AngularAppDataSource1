import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatDec'
})
export class DecNumberPipe implements PipeTransform {
  transform(value: number): string {
    let str = '';
    if (value) {
      const fraction = ',';
      const separator = '';
      str = value.toLocaleString('en-US');
      str = str.replace(/,/g, separator);
      str = str.replace(/\./, fraction);
    }    
    return str;
  }
}
