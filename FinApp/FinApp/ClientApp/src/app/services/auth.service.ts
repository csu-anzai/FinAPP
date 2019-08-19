import { NotificationService } from './notification.service';
import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, catchError, tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { MessagingCenterService } from './messaging-center.service';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { ErrorHandlingService } from './error-handling.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnInit {

  private isLogging: boolean;
  private loggedInStatus: boolean;
  private jwtHelper: JwtHelperService;
  private loggedInSubject: BehaviorSubject<any>;

  baseUrl = 'https://localhost:44397/api/';
  signInParameter = 'auth/';
  signUpParameter = 'user/';
  withGoogle = 'signingoogle/';
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
    private oauthService: OAuthService,
    private message: MessagingCenterService,
    private router: Router,
    private alertService: NotificationService,
    private errorHandler: ErrorHandlingService) {
    this.jwtHelper = new JwtHelperService();
    this.loggedInSubject = new BehaviorSubject<any>(null);
  }

  ngOnInit(): void {
    this.isLogging = false;
    this.loggedInStatus = this.loggedIn();
  }

  login(model: any) {
    return this.http.post(this.baseUrl + this.signInParameter + 'signin', model)
      .pipe(
        map((response: any) => {
          if (response.token) {
            const user = response;
            if (user) {
              this.setLoggedIn(true);
              this.cookieService.set('token', user.token, null, null, null, true);
              this.decodedToken = this.jwtHelper.decodeToken(user.token);
            }
          } else if (response.error) {
            throw new Error(response.error);
          }
        })
      );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + this.signUpParameter + 'signup', model)
      .pipe(
        tap(
          (response: any) => {
            if (response && response.error) {
              throw new Error(response.error);
            }
          },
          error => {
            this.errorHandler.handleError(error);
            return error;
          }
        ));
  }

  // in token service
  organizeGoogleAuthFlow() {
    if (this.cookieService.check('idToken')) {
      const token = this.cookieService.get('idToken');
      if (this.jwtHelper.isTokenExpired(token)) {
        this.cookieService.delete('idToken');
      } else {
        this.getDataFromTokenId(token);
      }
    } else {
      this.oauthService.initLoginFlow();
    }
  }

  // in token service
  getDataFromTokenId(tokenId: string): any {
    return this.http.post(this.baseUrl + this.signInParameter + this.withGoogle, { 'idToken': tokenId })
      .toPromise()
      .then(
        (response: any) => {
          // User already exists
          if (response.token) {
            this.cookieService.set('token', response.token, null, null, null, true);

            this.cookieService.set('idToken', tokenId, null, null, null, true);
            this.decodedToken = this.jwtHelper.decodeToken(response.token);
            this.alertService.successMsg('Logged in successfuly');
            this.setLoggedIn(true);
            this.router.navigate(['user/profile']);
            return true;
          } // Passes data to the sign up page
          else if (response.googleProfile) {
            const user = response.googleProfile;
            const queryParams = {
              email: user.email,
              name: user.name,
              surname: user.surname
            };
            this.message.passParameters(queryParams);
            this.router.navigate(['sign-up']);
            return true;
          } else if (response.code) {
            return this.alertService.errorMsg(response.message);
          }
          return false;
        }
      ).catch(error => {
        this.alertService.errorMsg(error.message);
      });
  }

  // in token service
  refreshToken() {
    return this.http.post(this.baseUrl + 'token', { accessToken: this.cookieService.get('token') })
      .pipe(
        tap((data: any) => {
          // Update token
          this.cookieService.delete('token', '/');
          this.cookieService.delete('token', '/user');
          this.cookieService.set('token', data.token);
          return data.token;
        }),
        catchError(err => throwError(err))
      );
  }

  isTokenActive() {
    const isTokenAvailable = this.loggedIn();
    if (isTokenAvailable) {
      const token = this.cookieService.get('token');
      const isLoggedIn = !this.jwtHelper.isTokenExpired(token);
      this.loggedInStatus = isLoggedIn;
      return isLoggedIn;
    }
    return false;
  }

  loggedIn() {
    return this.cookieService.check('token');
  }


  // Check if access token expires
  // loggedIn() {
  //   const isAvailable = this.cookieService.check('token');
  //   if (isAvailable) {
  //     const token = this.cookieService.get('token');
  //     const isExpired = this.jwtHelper.isTokenExpired(token);
  //     if (isExpired) {
  //       if (!this.isLogging) {
  //         this.isLogging = true;
  //         this.loggedInSubject.next(null);
  //         this.refreshToken()
  //           .pipe(
  //             catchError(err => throwError(err))
  //           )
  //           .subscribe(
  //             (data) => {
  //               // Update token
  //               this.isLogging = false;
  //               this.loggedInSubject.next(data);
  //               return !this.jwtHelper.isTokenExpired(data);
  //             }
  //           );
  //       }
  //     }
  //     return !isExpired;
  //   }
  //   return false;
  // }

  logOut() {
    this.setLoggedIn(false);
    this.oauthService.logOut(true);
    this.cookieService.delete('token', '/');
    this.cookieService.delete('token', '/user');
    // this.cookieService.delete('idToken', '/');
    this.router.navigate(['']);
    this.alertService.infoMsg('Logged Out');
  }
}
