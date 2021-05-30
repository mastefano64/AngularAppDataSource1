import { Directive } from '@angular/core';
import { AbstractControl, Validator, NG_VALIDATORS } from '@angular/forms';

@Directive({
  selector: '[validInt][ngModel]',
  providers: [
    { provide: NG_VALIDATORS, useExisting: IntValidatorDirective, multi: true }
    //{ provide: NG_VALIDATORS, useExisting: forwardRef(() => IntValidatorDirective), multi: true }
  ]
})
export class IntValidatorDirective implements Validator {
  validate(c: AbstractControl): { [key: string]: any } {
    const regexp = '^[0-9]+$';
    // const regexp = '^[0-9]+(\.[0-9]{1,2})?$';
    if (c.value) {
      const v = new String(c.value);
      if (!v.match(regexp)) {
        return { 'invalidInt': true };
      }
    }
    return null;
  }
}
