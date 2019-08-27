import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class EmailConfirmationService {

  constructor(private http: HttpClient) { }

  baseUrl = 'https://localhost:44397/api/emailConfirmation';

  sendConfirmEmailLink(user: User) {
    let callbackUrl = window.location.origin + '/confirm-email-success?token=';
    let model = {
      userId: user.id,
      callbackUrl: callbackUrl
    };

    return this.http.post(this.baseUrl + '/sendConfirmEmailLink', model);
  }

  validateConfirmEmailLink(model:any) {

    return this.http.post(this.baseUrl + '/validateEmailLink', model);
  }

}
