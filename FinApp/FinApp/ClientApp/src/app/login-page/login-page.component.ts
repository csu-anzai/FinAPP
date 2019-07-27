import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';

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
    this.authService.login(this.model).subscribe( () => {
      this.router.navigate(['fetch-data']);
    });
  }

  // onLogout() {
  //   localStorage.removeItem('token');
  //   this.router.navigate(['/home']);
  // }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
