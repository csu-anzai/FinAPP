import { Injectable, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';

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
  }

  successMsg(content: string) {
    this.toastr.success(content);
  }
  errorMsg(content: string) {
    this.toastr.error(content);

  }
  infoMsg(content: string) {
    this.toastr.info(content);
  }
  waringMsg(content: string) {
    this.toastr.warning(content);
  }
}
