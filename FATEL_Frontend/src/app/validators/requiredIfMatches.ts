import {AbstractControl, ValidatorFn} from '@angular/forms';

export function requiredIfMatches(actualValue: any, expectedValue: any): ValidatorFn {
  return (control: AbstractControl): {[key: string]: any} | null => {
    const actual = control.parent?.get(actualValue)?.value;
    const forbidden = actual === expectedValue && control.value === '';
    return forbidden ? {'requiredIfMatches': {value: control.value}} : null;
  };
}
