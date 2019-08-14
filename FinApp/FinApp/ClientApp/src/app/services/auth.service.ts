import { NotificationService } from './notification.service';
import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, catchError, mergeMap, share, tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { MessagingCenterService } from './messaging-center.service';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable, throwError, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnInit {
  private loggedInStatus = false;

  baseUrl = 'https://localhost:44397/api/';
  signInParameter = 'auth/';
  signUpParameter = 'user/';
  withGoogle = 'signingoogle/';

  jwtHelper = new JwtHelperService();
  private isLogging = false;
  private loggedInSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
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

  isSelectAccount() {
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
          }
          return false;
        }
      ).catch(error => {
        this.oauthService.initLoginFlow();
      });
  }

  refreshToken() {
    console.log('zaishlo');
    return this.http.post('https://localhost:44397/api/token', { accessToken: this.cookieService.get('token') });
    // .subscribe(
    //     (data) => {
    //       // Update token
    //       console.log('new token: ' + data.token);
    //       this.cookieService.set('token', data.token);
    //     }
    // );
  }

  // Clone our fieled request ant try to resend it

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
      const isExpired = this.jwtHelper.isTokenExpired(token);
      if (isExpired) {
        if (!this.isLogging) {
          console.log('!refreshing');
          this.isLogging = true;
          this.loggedInSubject.next(null);
          this.refreshToken()
            .pipe(
              catchError(err => throwError(err))
            )
            .subscribe(
              (data: any) => {
                // Update token
                console.log('new token: ' + data.token);
                this.cookieService.set('token', data.token);
                this.isLogging = false;
                this.loggedInSubject.next(this.cookieService.get('token'));
                return !this.jwtHelper.isTokenExpired(data.token);
              }
            );
        }
      }
      return !this.jwtHelper.isTokenExpired(token);
    }
    return false;
  }
}
