import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationService } from 'src/app/services/notification.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { OAuthService } from 'angular-oauth2-oidc';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  private loginMsg: string;

  signInForm: FormGroup;
  googleTokenId?: string;
  loading = false;

  constructor(private authService: AuthService,
    private router: Router,
    private oauthService: OAuthService,
    fb: FormBuilder,
    private translate: TranslateService,
    private alertService: NotificationService) {
    this.signInForm = fb.group({
      'Email': new FormControl('', Validators.compose([Validators.required, Validators.email])),
      'Password': new FormControl('', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(16)])),
    });
  }

  ngOnInit() {

      this.translate.get('notifications.loggedInSuccessfullyMsg').subscribe
        (
          (text: string) => { this.loginMsg = text; console.log(this.loginMsg); }

        );
  }

  onLogin() {
    this.loading = true;
    this.authService.login(this.signInForm.value).subscribe(
      next => { },
      error => {
        this.loading = false;
        this.alertService.errorMsg(error.message);
      },
      () => {
        this.loading = false;
        this.authService.setLoggedIn(true);
        this.alertService.successMsg(this.loginMsg);
        this.router.navigate(['user/profile']);
      });
  }

  googleSignIn() {
    this.authService.organizeGoogleAuthFlow();
  }

  get f() { return this.signInForm.controls; }

  loggedIn() {
    return this.authService.isLoggedIn;
  }

  public getClaims() {
    const claims = this.oauthService.getIdentityClaims();
    if (!claims) {
      return null;
    }
    console.log(claims);
  }
}
