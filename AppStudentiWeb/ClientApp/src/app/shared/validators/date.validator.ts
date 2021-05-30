import { Directive } from '@angular/core';
import { AbstractControl, Validator, NG_VALIDATORS } from '@angular/forms';
import { Utility } from '../../app-core/utility';

@Directive({
  selector: '[validDat][ngModel]',
  providers: [
    { provide: NG_VALIDATORS, useExisting: DateValidatorDirective, multi: true }
    //{ provide: NG_VALIDATORS, useExisting: forwardRef(() => DecValidatorDirective), multi: true }
  ]
})
export class DateValidatorDirective implements Validator {
  validate(c: AbstractControl): { [key: string]: any } {
    if (c.value) {
      if (Utility.isDate(c.value) === false) {
        return { 'invalidDat': true };
      }
    }
    return null;
  }
}
