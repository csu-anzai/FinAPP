import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { tap } from 'rxjs/operators';
import { UserService } from './user.service'
import { Account } from '../models/account';
import { AuthService } from './auth.service';



@Injectable({
  providedIn: 'root'
})
export class ChartsService {

  url = 'https://localhost:44397/api/currency/';
  user: User = new User();
  accounts : Account[];

  constructor(private _userService: UserService, private _authService: AuthService,private http: HttpClient) { }
  ngOnInit() {
    this._userService.getUser(this._authService.DecodedToken.sub).subscribe(res => {
      this.accounts = this.user.accounts;
    });
    
  }
  getAccounts() : any {
    this._userService.getUser(this._authService.DecodedToken.sub).subscribe(res => {
      this.accounts = res.accounts;
      console.log(res.accounts);
      return res.accounts;
     });

     return this.accounts;
    // let res = await this._userService.getUser(this._authService.DecodedToken.sub);
    // console.log(res)
    // return res.accounts;
  }
    
}
