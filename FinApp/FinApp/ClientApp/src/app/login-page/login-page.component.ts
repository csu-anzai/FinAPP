import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { NotificationService } from '../_services/notification.service';


@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  model: any = {};
  signInForm: FormGroup;


  constructor(private authService: AuthService, private router: Router, fb: FormBuilder, private alertService: NotificationService)
  {
    this.signInForm = fb.group({
      'Email': new FormControl('', Validators.compose([Validators.required, Validators.email])),
      'Password': new FormControl('', Validators.required),
    });
  }

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
  get f() { return this.signInForm.controls; }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
