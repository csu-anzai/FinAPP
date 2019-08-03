import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { throwError } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'https://localhost:44397/api/';
  signInParameter = 'auth/';
  signUpParameter = 'user/';

  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient, private cookieService: CookieService, private alertService: NotificationService) { }

  login(model: any) {
    return this.http.post(this.baseUrl + this.signInParameter + 'signin', model)
      .pipe(
        map((response: any) => {
          console.log(response);

          console.log('opa');

          const user = response;
          if (user) {
            this.cookieService.set('token', user.token, null, null, null, true);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            return user;
          }
        })
      );
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
