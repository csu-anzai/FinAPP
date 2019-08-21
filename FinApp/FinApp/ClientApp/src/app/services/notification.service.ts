import { Injectable, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationService implements OnInit {
  successLoginMessage: string;
  logOutMessage: string;

  constructor(private toastr: ToastrService,
    private translate: TranslateService) { }

    ngOnInit() {
    //   this.translate.get('LoggedInSuccessfullyMsg').subscribe
    //   (
    //     (text: string) =>
    //     {
    //       this.successLoginMessage = text;
    //       console.log('-----');
    //       console.log(text);
    //     }
    //   );
    // this.translate.get('LogOutMsg').subscribe
    //   (
    //     (text: string) => { this.logOutMessage = text; console.log('-----'); console.log(text); }
    //   );
     }

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
