import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private toastr: ToastrService) { }

  successMsg(content: string) {
    this.toastr.success(content, 'Success');
  }
  errorMsg(content: string) {
    this.toastr.error(content, 'Error');

  }
  infoMsg(content: string) {
    this.toastr.info(content, 'Information');
  }
  waringMsg(content: string) {
    this.toastr.warning(content, 'Warning');
  }
}
