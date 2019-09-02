import { Component, OnInit, NgZone, AfterViewInit, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationService } from 'src/app/services/notification.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { OAuthService } from 'angular-oauth2-oidc';
import { EmailConfirmationService } from '../../services/email-confirmation.service';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { GoogleSignInSuccess } from 'angular-google-signin';
import { CookieService } from 'ngx-cookie-service';
import { HttpRequest } from '@angular/common/http';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit, AfterViewInit {
  private myClientId = '112578784048-unbg6n7pt2345q5m7i53u20pu7rj80dt.apps.googleusercontent.com';
  private loginMsg: string;

  auth2: any;
  signInForm: FormGroup;
  googleTokenId?: string;
  loading = false;

  constructor(private authService: AuthService,
    private router: Router,
    private emailConfirmationService: EmailConfirmationService,
    fb: FormBuilder,
    private translate: TranslateService,
    private oauthService: OAuthService,
    private element: ElementRef,
    private zone: NgZone,
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

  ngAfterViewInit() {
    this.googleInit();
  }

  onLogin() {
    console.log('opachki');
    this.loading = true;
    this.authService.login(this.signInForm.value).subscribe(
      next => { },
      error => {
        this.loading = false;
        this.alertService.errorMsg(error.message);

        if (error.status == 403) {
          this.sendConfirmEmailLink();
        }
      },
      () => {
        this.loading = false;
        this.authService.setLoggedIn(true);
        this.alertService.successMsg(this.loginMsg);
        this.router.navigate(['user/profile']);
      });
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

  sendConfirmEmailLink() {
    let email = this.signInForm.controls['Email'].value;
    this.emailConfirmationService.sendConfirmEmailLink(email).subscribe(
      next => { },

      error => this.alertService.errorMsg(error.message),

      () => this.router.navigate(['send-confirm-email'])
    );
  }

  public googleInit() {
    gapi.load('auth2', () => {
      this.zone.run(() => {
      this.auth2 = gapi.auth2.init({
        client_id: this.myClientId,
        cookie_policy: 'single_host_origin',
        scope: 'profile email'
      });
      this.attachSignin(document.getElementById('googleBtn'));
    });
  });
  }

  public attachSignin(element) {
    this.auth2.attachClickHandler(element, {},
      (googleUser) => {
        this.zone.run(() => {
        const idToken = googleUser.getAuthResponse().id_token;
        this.authService.getDataFromTokenId(idToken);
      }, function (error) {
        console.log(JSON.stringify(error, undefined, 2));
      });
    });
  }

  // onGoogleSignInSuccess(event: GoogleSignInSuccess) {
  //   this.zone.run(() => {
  //   const googleUser: gapi.auth2.GoogleUser = event.googleUser;
  //   const idToken = googleUser.getAuthResponse().id_token;
  //   console.log(idToken);
  //   this.authService.getDataFromTokenId(idToken);
  //   });
  // }
}
