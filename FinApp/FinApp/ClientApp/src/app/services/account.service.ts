import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Account } from '../models/account';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { AuthService } from './auth.service'
import { AccountAdd } from '../models/accountAdd';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  url = 'https://localhost:44397/api/account/';

  constructor(public http: HttpClient, private authService: AuthService) { }

  getAccount(id: number): Observable<Account> {
    return this.http.get<Account>(this.url + this.authService.DecodedToken.sub + "/" + id)
      .pipe(tap((data: Account) => {
        return data;
      }));
  }

  addAccount(model: AccountAdd) {
    model.userId = this.authService.DecodedToken.sub;
    console.log(model);
    return this.http.post(this.url + 'add', model)
      .pipe(data => {
        return data;
      });
  }
}
