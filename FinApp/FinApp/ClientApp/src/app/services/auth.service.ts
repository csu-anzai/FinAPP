import { NotificationService } from './notification.service';
import { Injectable, OnInit } from '@angular/core';
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
// import { DataService } from '../common/data.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class CustomAuthService implements OnInit {
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
    // private data: DataService,
    private router: Router,
    private alertService: NotificationService) { }

  ngOnInit(): void {
  }

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

  signInWithGoogle(): any {
    return this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID).then(
      user => {
        console.log(user);
        return this.getDataFromTokenId(user.idToken);
      }
    );
  }

  getDataFromTokenId(tokenId: string): any {
        return this.http.post(`https://localhost:44397/api/auth/signingoogle/`, {'idToken': tokenId}).toPromise()
          .then(
            (response: any) => {
              if (response.token) {
                this.cookieService.set('token', response.token, null, null, null, true);
                this.decodedToken = this.jwtHelper.decodeToken(response.token);
              // } else if (response.googleProfile) {
              //   throwError(response.message);
              //   this.router.navigate(['sign-up']);
              } else if (response.code = 401) {
                this.router.navigate(['sign-up']);
              }
              return response;
            }
          ).catch(error => {
            console.log(error);
            // throwError(error);
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
