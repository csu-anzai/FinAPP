import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  onLogin() {
    this.authService.login(this.model).subscribe(
      next => {
        console.log('Logged in successfuly');
      },
      error => {
        console.log(error);
      },
      () => {
        this.router.navigate(['fetch-data']);
      });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
