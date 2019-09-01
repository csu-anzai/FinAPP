import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';
import { NotificationService } from 'src/app/services/notification.service';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { ChangePassword } from 'src/app/models/changePassword';
import { EmailConfirmationService } from 'src/app/services/email-confirmation.service';
import { Email } from 'src/app/models/email';

@Component({
  selector: 'app-contact-us',
  templateUrl: './contact-us.component.html',
  styleUrls: ['./contact-us.component.css']
})
export class ContactUsComponent implements OnInit {
  emailInfo: Email;
  contactForm: FormGroup;

  constructor(
    private _authService: AuthService,
    private _userService: UserService,
    private emailSender: EmailConfirmationService,
    private formBuilder: FormBuilder,
    private _alertService: NotificationService) {


    this.contactForm = formBuilder.group({
      'FullName': new FormControl('', Validators.compose(
        [Validators.required, Validators.pattern("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$")])),

      'Email': new FormControl('', Validators.compose(
        [Validators.required, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$')])),

      'Message': new FormControl('', Validators.required)

    });
  }

  ngOnInit() {
    this.emailInfo = new Email();
    this._userService.getUser(this._authService.DecodedToken.sub).subscribe(user => {
      this.contactForm.get('FullName').setValue(`${user.name} ${user.surname}`);
      this.contactForm.get('Email').setValue(user.email);
    });
  }

  onSendMessage() {
    if (this.contactForm.valid) {
      console.log('good');
      
      this.emailInfo.email = this.contactForm.controls['Email'].value;
      this.emailInfo.message = this.contactForm.controls['Message'].value;
      this.emailInfo.subject = 'Contact form';
      // this.emailSender.sendMailToAdmin(this.emailInfo).subscribe(
      //   () => this._alertService.infoMsg('Your message was sent')
      // );
    }
  }
}
