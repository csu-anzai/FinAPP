import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ForgotPasswordService } from '../../services/forgot.password.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  passwordForm: FormGroup;
  newPassword;

  constructor(
    private router: Router,
    private forgotPasswordService: ForgotPasswordService,
    private userService: UserService,
    formBuilder: FormBuilder) {
    this.passwordForm = formBuilder.group({
      'Password': new FormControl('', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(16)])),
      'RepeatedPassword': new FormControl('', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(16)])),
    });
    this.passwordForm.valueChanges.subscribe(field => {
      if (this.passwordForm.controls['RepeatedPassword'].value != this.passwordForm.controls['Password'].value) {
        this.passwordForm.controls['RepeatedPassword'].setErrors({ mismatch: true });
      }
      else {
        this.passwordForm.controls['RepeatedPassword'].setErrors(null);
      }
    });
  }

  ngOnInit() {
  }

  onPasswordChange() {
    if (this.passwordForm.valid) {
      this.newPassword = this.passwordForm.controls['Password'].value,

        this.forgotPasswordService.changePassword(this.newPassword).subscribe(() => {
          this.router.navigate(['login-page']);
        });
    }
    else {
        for (let i in this.passwordForm.controls)
          this.passwordForm.controls[i].markAsTouched();
    }
  }
}
