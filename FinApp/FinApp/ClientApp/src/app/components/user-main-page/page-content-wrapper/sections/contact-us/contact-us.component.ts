import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';
import { NotificationService } from 'src/app/services/notification.service';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { EmailConfirmationService } from 'src/app/services/email-confirmation.service';
import { Email } from 'src/app/models/email';
import { LangChangeEvent, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-contact-us',
  templateUrl: './contact-us.component.html',
  styleUrls: ['./contact-us.component.css']
})
export class ContactUsComponent implements OnInit {
  emailInfo: Email;
  contactForm: FormGroup;
  successfullySent: string;
  failedSent: string;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private emailSender: EmailConfirmationService,
    private formBuilder: FormBuilder,
    private translate: TranslateService,
    private alertService: NotificationService) {
    this.setUpContactForm();
    this.getTranslation();
    this.translateSubscription();
  }

  ngOnInit() {
    this.getUserInfo();
  }

  onSendMessage() {
    if (this.contactForm.valid) {
      this.emailInfo.fullName = this.contactForm.controls['FullName'].value;
      this.emailInfo.email = this.contactForm.controls['Email'].value;
      this.emailInfo.message = this.contactForm.controls['Message'].value;

      this.emailSender.sendMailToAdmin(this.emailInfo).subscribe(
        next => { },
        err => this.alertService.errorMsg(this.failedSent),
        () => this.alertService.infoMsg(this.successfullySent)
      );
    }
  }

  setUpContactForm() {
    this.contactForm = this.formBuilder.group({
      'FullName': new FormControl('', Validators.compose(
        [Validators.required, Validators.pattern('^[a-zA-Z]+(([\',. -][a-zA-Z ])?[a-zA-Z]*)*$')])),

      'Email': new FormControl('', Validators.compose(
        [Validators.required, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$')])),

      'Message': new FormControl('', Validators.required)
    });
  }

  getUserInfo() {
    this.emailInfo = new Email();

    this.userService.getUser(this.authService.DecodedToken.sub).subscribe(user => {
      this.contactForm.get('FullName').setValue(`${user.name} ${user.surname}`);
      this.contactForm.get('Email').setValue(user.email);
    });
  }

  getTranslation() {
    const successKey = 'contactUs.successfullyMsg';
    const failureKey = 'contactUs.errorMsg';

    this.translate.get([successKey, failureKey])
      .subscribe(translations => {
        this.successfullySent = translations[successKey];
        this.failedSent = translations[failureKey];
      });
  }

  translateSubscription() {
    this.translate.onLangChange.subscribe((event: LangChangeEvent) => {
      this.getTranslation();
    });
  }
}
