import {AbstractControl, FormControl, ValidatorFn, Validators} from '@angular/forms';

export function requiredIfMatches(actualValue: any, expectedValue: any): ValidatorFn {
  //make form control required if actual value matches expected value
  return (control: AbstractControl): { [key: string]: any } | null => {
    console.log(actualValue, expectedValue, actualValue === expectedValue)
    if (actualValue === expectedValue) {
      return Validators.required(control);
    }
    return null;
  }
}
