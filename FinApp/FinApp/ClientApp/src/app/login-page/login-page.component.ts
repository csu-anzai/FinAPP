import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { NotificationService } from '../_services/notification.service';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService, private router: Router, private alertService: NotificationService) { }

  ngOnInit() {
  }

  onLogin() {
    this.authService.login(this.model).pipe(
      catchError(err => throwError(err))
    ).subscribe(
      next => {},
      error => throwError(error),
      () => {
        this.alertService.successMsg('Logged In');
        this.router.navigate(['fetch-data']);
      }
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
