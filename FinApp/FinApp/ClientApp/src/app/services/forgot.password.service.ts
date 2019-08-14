import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ForgotPassword } from '../models/forgotPassword';
import { User } from '../models/user';
import { map, tap } from 'rxjs/operators';
import { PasswordConfirmationCode } from '../models/passwordConfirmationCode';
import { pipe } from 'rxjs';
import { UserService } from './user.service';
import { RecoverPassword } from '../models/recoverPassword';

@Injectable()
export class ForgotPasswordService {
  baseUrl = 'https://localhost:44397/api/passwordRecovery/';
  userId: number;
  enteredCodeIsValid: boolean;

  constructor(private http: HttpClient, private userService: UserService) { }

  sendConfirmCodeToUser(model: ForgotPassword) {
    return this.http.post(this.baseUrl + 'sendCode', model)
      .pipe(
        map((response: any) => {
          this.userId = response.id;
        })
      );
    }

  validateCode(enteredCode: string) {
    let codeOfUser = new PasswordConfirmationCode();
    codeOfUser.userId = this.userId;
    codeOfUser.code = enteredCode;
    return this.http.post(this.baseUrl + 'validateCode', codeOfUser)
      .pipe(
        map((response: any) => {
          this.enteredCodeIsValid = response;
        })
    );
  }

  changePassword(newPassword: string) {
    let recoverPasswordModel = new RecoverPassword();
    recoverPasswordModel.id = this.userId;
    recoverPasswordModel.newPassword = newPassword;

    return this.userService.recoverPassword(recoverPasswordModel);
  }
}
