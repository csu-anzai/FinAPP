import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class EmailConfirmationService {

  constructor(private http: HttpClient) { }

  baseUrl = 'https://localhost:44397/api/emailConfirmation';
  callbackUrl = window.location.origin + '/confirm-email-success?token=';
  isValidLink: boolean;

  sendConfirmEmailLink(email: string) {
    let model = {
      userEmail: email,
      callbackUrl: this.callbackUrl
    };

    return this.http.post(this.baseUrl + '/sendConfirmEmailLink', model);
  }

  validateConfirmEmailLink(model:any) {

    return this.http.post(this.baseUrl + '/validateEmailLink', model).pipe(
      map((response: any) => {
        this.isValidLink = response;
        })
      );
  }
}
