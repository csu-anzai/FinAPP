import { Injectable, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationService implements OnInit {
  successTitle: string;
  errorTitle: string;
  infoTitle: string;
  warningTitle: string;

  constructor(private toastr: ToastrService,
    private translate: TranslateService) { }

  ngOnInit() {
    this.translate.get('notifications.successMsg').subscribe((text: string) => this.successTitle = text);
    this.translate.get('notifications.errorMsg').subscribe((text: string) => this.errorTitle = text);
    this.translate.get('notifications.infoMsg').subscribe((text: string) => this.infoTitle = text);
    this.translate.get('notifications.warningMsg').subscribe((text: string) => this.warningTitle = text);
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
