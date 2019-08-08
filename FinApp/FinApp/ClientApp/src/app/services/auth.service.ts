import { NotificationService } from './notification.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { throwError, Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import {
  AuthService,
  GoogleLoginProvider,
  SocialUser
} from 'angular-6-social-login';

@Injectable({
  providedIn: 'root'
})
export class CustomAuthService {
  private loggedInStatus = false;
  baseUrl = 'https://localhost:44397/api/';
  signInParameter = 'auth/';
  signUpParameter = 'user/';

  jwtHelper = new JwtHelperService();
  decodedToken: any;

  setLoggedIn(value: boolean) {
    this.loggedInStatus = value;
  }

  get isLoggedIn() {
    return this.loggedInStatus;
  }

  get DecodedToken() {
    return this.jwtHelper.decodeToken(this.cookieService.get('token'));
  }

  constructor(private http: HttpClient,
    private socialAuthService: AuthService,
    private cookieService: CookieService,
    private alertService: NotificationService) { }

  login(model: any) {
    try {
      return this.http.post(this.baseUrl + this.signInParameter + 'signin', model)
        .pipe(
          map((response: any) => {
            const user = response;
            if (user) {
              this.cookieService.set('token', user.token, null, null, null, true);
              this.decodedToken = this.jwtHelper.decodeToken(user.token);
            }
          })
        );
    } catch (error) {
      this.alertService.errorMsg(error.message);
    }
  }

  signInWithGoogle() {
    return this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID).then(
      (userData) => {
        console.log(userData);
        return userData;
      });
  }

  register(model: any) {

    try {
      return this.http.post(this.baseUrl + this.signUpParameter + 'signup', model);
    } catch (error) {
      this.alertService.errorMsg(error.message);
    }
  }

  // Check if access token expires
  loggedIn() {
    const isAvailable = this.cookieService.check('token');

    if (isAvailable) {
      const token = this.cookieService.get('token');
      return !this.jwtHelper.isTokenExpired(token);
    }

    return false;
  }
}
