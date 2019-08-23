import { Component, OnInit } from '@angular/core';
import { ChangePassword } from 'src/app/models/changePassword';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css']
})
export class SettingComponent implements OnInit {

  changePasswordForm: FormGroup;

  changePassword: ChangePassword = new ChangePassword;

  constructor(
    private _authService: AuthService,
    private _userService: UserService,
    fb: FormBuilder,
    private _alertService: NotificationService) {


    this.changePasswordForm = fb.group({
      'OldPassword': new FormControl('', Validators.compose([Validators.required])),
      'Password': new FormControl('', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(16)])),
      'ConfirmPassword': new FormControl('', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(16)])),

    });
    this.changePasswordForm.valueChanges.subscribe(field => {
      if (this.changePasswordForm.controls['Password'].value !== this.changePasswordForm.controls['ConfirmPassword'].value) {
        this.changePasswordForm.controls['ConfirmPassword'].setErrors({ mismatch: true });
      } else {
        this.changePasswordForm.controls['ConfirmPassword'].setErrors(null);
      }
    });
  }

  ngOnInit() {





  }
  updatePassword() {
    if (this.changePasswordForm.valid) {
      this.changePassword.oldPassword = this.changePasswordForm.controls["OldPassword"].value,
        this.changePassword.password = this.changePasswordForm.controls["Password"].value,
        this.changePassword.confirmPassword = this.changePasswordForm.controls["ConfirmPassword"].value,
        this.changePassword.userId = this._authService.DecodedToken.sub;
      this._userService.updatePassword(this.changePassword).subscribe(
        data => this._alertService.successMsg("Your Pass was updated"),
       error => this._alertService.errorMsg(error.error.error));         
    }
  }

}
