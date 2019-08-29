import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationService } from 'src/app/services/notification.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { OAuthService } from 'angular-oauth2-oidc';
import { EmailConfirmationService } from '../../services/email-confirmation.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  signInForm: FormGroup;
  googleTokenId?: string;

  loading = false;

  constructor(private authService: AuthService,
    private router: Router,
    private oauthService: OAuthService,
    private alertService: NotificationService,
    private emailConfirmationService: EmailConfirmationService,
    fb: FormBuilder) {
    this.signInForm = fb.group({
      'Email': new FormControl('', Validators.compose([Validators.required, Validators.email])),
      'Password': new FormControl('', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(16)])),
    });
  }

  onLogin() {
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
        this.alertService.successMsg('Logged in successfuly');
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

  sendConfirmEmailLink() {
    let email = this.signInForm.controls['Email'].value;
    this.emailConfirmationService.sendConfirmEmailLink(email).subscribe(
      next => { },

      error => this.alertService.errorMsg(error.message),

      () => this.router.navigate(['send-confirm-email'])
    );
  }
}
