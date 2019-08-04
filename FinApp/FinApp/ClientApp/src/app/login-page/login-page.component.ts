import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { NotificationService } from '../_services/notification.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService, private router: Router,  private alertService: NotificationService) { }

  ngOnInit() {
  }

  onLogin() {
    this.authService.login(this.model).subscribe(
      next => {},
      error => {
        this.alertService.errorMsg(error.message);
      },
      () => {
        this.alertService.successMsg('Logged in successfuly');
        this.router.navigate(['fetch-data']);
      });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
