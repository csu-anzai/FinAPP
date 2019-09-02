import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { MessagingCenterService } from '../../services/messaging-center.service';
import { NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { NotificationService } from 'src/app/services/notification.service';
import { EmailConfirmationService } from '../../services/email-confirmation.service';
@Component({
  selector: 'sign-up-component',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  imageUrl: string;
  isConfirmed: boolean;
  signUpForm: FormGroup;
  user: any;

  constructor(
    private router: Router,
    private authService: AuthService,
    private message: MessagingCenterService,
    private alertService: NotificationService,
    private parserFormatter: NgbDateParserFormatter,
    private emailConfirmationService: EmailConfirmationService,
    fb: FormBuilder) {
    this.signUpForm = fb.group({
      'Name': new FormControl('', Validators.compose([Validators.required, Validators.pattern('[a-zA-z-]*')])),
      'BirthDate': new FormControl('', Validators.required),
      'Surname': new FormControl('', Validators.compose([Validators.required, Validators.pattern('[a-zA-z-]*')])),
      'Email': new FormControl('', Validators.compose([Validators.required, Validators.pattern("[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")])),
      'Password': new FormControl('', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(16)])),
      'RepeatedPassword': new FormControl('', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(16)])),
    });
    this.signUpForm.valueChanges.subscribe(field => {
      if (this.signUpForm.controls['Password'].value !== this.signUpForm.controls['RepeatedPassword'].value) {
        this.signUpForm.controls['RepeatedPassword'].setErrors({ mismatch: true });
      } else {
        this.signUpForm.controls['RepeatedPassword'].setErrors(null);
      }
    });
  }

  ngOnInit(): void {
    this.message.currentParams.subscribe(
      (obj: any) => {
        this.signUpForm.controls['Email'].setValue(obj.email);
        this.signUpForm.controls['Name'].setValue(obj.name);
        this.signUpForm.controls['Surname'].setValue(obj.surname);
        this.imageUrl = obj.avatar;
        this.isConfirmed = obj.isConfirmed;
      }
    );
  }

  onSignUp() {
    if (this.signUpForm.valid) {
      const registrationModel = {
        Name: this.signUpForm.controls['Name'].value,
        Surname: this.signUpForm.controls['Surname'].value,
        BirthDate: this.parserFormatter.format( this.signUpForm.controls['BirthDate'].value),
        Email: this.signUpForm.controls['Email'].value,
        Password: this.signUpForm.controls['Password'].value,
        Avatar: this.imageUrl,
        CallbackUrlForEmailConfirm: this.emailConfirmationService.callbackUrl
      };
      this.authService.register(registrationModel).subscribe(
        next => { },
        error => this.alertService.errorMsg(error),
        () => this.router.navigate(['send-confirm-email'])
      );
    } else {
      for (let i in this.signUpForm.controls)
        this.signUpForm.controls[i].markAsTouched();
    }
  }
}
