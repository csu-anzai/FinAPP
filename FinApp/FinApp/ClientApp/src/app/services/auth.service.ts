import { NotificationService } from './notification.service';
import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { throwError, Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { DataService } from '../common/data.service';

@Injectable({
  providedIn: 'root'
})
export class CustomAuthService implements OnInit {
  private loggedInStatus = false;

  baseUrl = 'https://localhost:44397/api/';
  signInParameter = 'auth/';
  signUpParameter = 'user/';
  withGoogle = 'signingoogle/';

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
    private cookieService: CookieService,
    private data: DataService,
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

  getDataFromTokenId(tokenId: string): any {
    return this.http.post(this.baseUrl + this.signInParameter + this.withGoogle, { 'idToken': tokenId })
    .toPromise()
      .then(
        (response: any) => {
          // User already exists
          if (response.token) {
            this.cookieService.set('token', response.token, null, null, null, true);
            this.decodedToken = this.jwtHelper.decodeToken(response.token);
            this.router.navigate(['sign-up']);

          } // Passes data to the sign up page
          else if (response.googleProfile) {
            const user = response.googleProfile;
            const queryParams = {
              email: user.email,
              name: user.name,
              surname: user.surname
            };
            this.data.passParameters(queryParams);
            this.router.navigate(['sign-up']);
          }

          return response;
        }
      ).catch(error => {
        throwError(error);
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
