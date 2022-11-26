import {AbstractControl, ValidationErrors, ValidatorFn} from "@angular/forms";

export function greaterThanDirective(): ValidatorFn {
  return (control:AbstractControl) : { [key: string]: boolean } | null => {
    const value = control.value;

    if(value <= 0)
      return {'greaterThan': true};

    return null;
  };
}
