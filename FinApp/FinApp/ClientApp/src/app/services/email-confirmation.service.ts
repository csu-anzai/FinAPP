import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmailConfirmationService {

  constructor(private http: HttpClient) { }

  baseUrl = 'https://localhost:44397/api/emailInteraction';
  callbackUrl = window.location.origin + '/confirm-email-success?token=';

  sendConfirmEmailLink(email: string) {
    let model = {
      userEmail: email,
      callbackUrl: this.callbackUrl
    };

    return this.http.post(this.baseUrl + '/sendConfirmEmailLink', model);
  }

  validateConfirmEmailLink(model:any) {

    return this.http.post(this.baseUrl + '/validateEmailLink', model);
  }
}
