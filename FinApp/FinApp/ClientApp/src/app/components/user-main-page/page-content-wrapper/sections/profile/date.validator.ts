import { AbstractControl } from '@angular/forms';
import * as moment from 'moment';

export class DateValidator {
  static dateVaidator(AC: AbstractControl) {
    if (AC && AC.value && !moment(AC.value, 'dd-mm-yyyy', true).isValid()) {
      return { 'dateVaidator': true };
    }
    return null;
  }
}
