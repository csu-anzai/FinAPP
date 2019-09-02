import { NotificationService } from './notification.service';
import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, catchError, tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { MessagingCenterService } from './messaging-center.service';
import { OAuthService } from 'angular-oauth2-oidc';
import { throwError, BehaviorSubject, Observable } from 'rxjs';
import { ErrorHandlingService } from './error-handling.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnInit {

  private loggedInStatus: boolean;
  private jwtHelper: JwtHelperService;

  baseUrl = 'https://localhost:44397/api/';
  signInParameter = 'auth/';
  signUpParameter = 'users/';
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
  }

  ngOnInit(): void {
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
              this.cookieService.set('token', user.token, null, '/', null, true);
              this.decodedToken = this.jwtHelper.decodeToken(user.token);
            }
          } else if (response.error) {
            throw new Error(response.error);
          }
        })
      );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + this.signUpParameter, model)
      .pipe(
        tap(
          (response: any) => {
            if (response && response.error) {
              throw new Error(response.error);
            }
          },
          error => {
            this.errorHandler.handleError(error);
          }
        ));
  }

  // in token service
  // organizeGoogleAuthFlow() {
  //   if (this.cookieService.check('idToken')) {
  //     const token = this.cookieService.get('idToken');
  //     if (this.jwtHelper.isTokenExpired(token)) {
  //       this.cookieService.delete('idToken');
  //     } else {
  //       this.getDataFromTokenId(token);
  //     }
  //   } else {
  //     this.oauthService.initLoginFlow();
  //   }
  // }

  // // in token service
  getDataFromTokenId(tokenId: string): Promise<any> {
    return this.http.post(this.baseUrl + this.signInParameter + this.withGoogle, { 'idToken': tokenId })
      .toPromise()
      .then(
        (response: any) => {
          // User already exists
          if (response.token) {
            this.cookieService.set('token', response.token, null, '/', null, true);
            this.cookieService.set('idToken', tokenId, null, '/', null, true);
            this.decodedToken = this.jwtHelper.decodeToken(response.token);
            this.setLoggedIn(true);
          } // Passes data to the sign up page
          else if (response.googleProfile) {
            const user = response.googleProfile;
            const queryParams = {
              email: user.email,
              name: user.name,
              surname: user.surname,
              avatar: user.avatar,
              isConfirmed: user.IsEmailConfirmed
            };
            this.message.passParameters(queryParams);
          } else if (response.code) {
            throwError(response.code);
          }
          return response;
        }
      ).catch(error => {
        throwError(error);
      });
  }

  // in token service
  refreshToken() {

    return this.http.post(this.baseUrl + 'tokens', { idToken: this.cookieService.get('token') })
      .pipe(
        tap((data: any) => {
          // Update token
          this.cookieService.delete('token', '/');
          this.cookieService.set('token', data.token, null, '/', null, true);
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

  // Don't touch it - so important !!!!
  // Check if access token expires
  // private loggedInSubject: BehaviorSubject<any>;
  // private isLogging: boolean;
  // ctor     this.loggedInSubject = new BehaviorSubject<any>(null);
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
    // this.cookieService.delete('idToken', '/');
    this.router.navigate(['']);
    this.alertService.infoMsg('Logged Out');
  }
}
