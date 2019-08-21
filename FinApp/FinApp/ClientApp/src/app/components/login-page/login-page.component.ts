import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationService } from 'src/app/services/notification.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { OAuthService } from 'angular-oauth2-oidc';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  model: any = {};
  signInForm: FormGroup;
  googleTokenId?: string;

  constructor(private authService: AuthService,
    private router: Router,
    private oauthService: OAuthService,
    fb: FormBuilder,
    private translate: TranslateService,
    private alertService: NotificationService) {
    this.signInForm = fb.group({
      'Email': new FormControl('', Validators.compose([Validators.required, Validators.email])),
      'Password': new FormControl('', Validators.required),
    });
  }

  onLogin() {
    this.authService.login(this.model).subscribe(
      next => { },
      error => {
        this.alertService.errorMsg(error.message);
      },
      () => {
        this.authService.setLoggedIn(true);
        this.alertService.successMsg('build sucess');
        this.router.navigate(['user/profile']);
      });
  }

  googleSignIn() {
    this.authService.isSelectAccount();
  }

  get f() { return this.signInForm.controls; }

  loggedIn() {
    return this.authService.loggedIn();
  }

  public getClaims() {
    const claims = this.oauthService.getIdentityClaims();
    if (!claims) {
      return null;
    }
    console.log(claims);
  }
}
