import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Account } from '../models/account';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthService } from './auth.service'
import { AccountAdd } from '../models/accountAdd';
import { NotificationService } from './notification.service';
import { ErrorHandlingService } from './error-handling.service';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  url = 'https://localhost:44397/api/accounts/';

  constructor(public http: HttpClient,
    private authService: AuthService,
    private alertService: NotificationService,
    private errorHandler: ErrorHandlingService) { }

  getAccount(id: number): Observable<Account> {
    return this.http.get<Account>(this.url + this.authService.DecodedToken.sub + "/" + id)
      .pipe(tap(
        (data: Account) => {
          return data;
        },
        error => this.errorHandler.handleError(error)
      ));
  }

  addAccount(model: AccountAdd) {
    model.userId = this.authService.DecodedToken.sub;
    return this.http.post(this.url + 'add', model)
      .pipe(tap( // Log the result or error
        data => {
          console.log(data);
          this.alertService.successMsg("Account added successful");
          return data;
        },
        error => { this.errorHandler.handleError(error); }
      ));
  }
}
