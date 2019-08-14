import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ForgotPasswordService } from 'src/app/services/forgot.password.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})

export class ForgotPasswordComponent implements OnInit {
  forgotPasswordForm: FormGroup;
  userEmail;

  constructor(
    private router: Router,
    private forgotPasswordService: ForgotPasswordService,
    formBuilder: FormBuilder) {
    this.forgotPasswordForm = formBuilder.group({
      'Email': new FormControl('', Validators.compose([Validators.required, Validators.pattern("[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")])),
    });
  }

  ngOnInit() {
  }

  onEmailSend() {
    if (this.forgotPasswordForm.valid) {
      this.userEmail = {
        Email: this.forgotPasswordForm.controls['Email'].value,
      },
        this.forgotPasswordService.sendConfirmCodeToUser(this.userEmail).subscribe(() => {
          this.router.navigate(['confirm-code']);
        });
    }
    else {
      for (let i in this.forgotPasswordForm.controls)
        this.forgotPasswordForm.controls[i].markAsTouched();
    }
  }
}
