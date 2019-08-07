import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CustomAuthService } from 'src/app/services/auth.service';
import { NotificationService } from 'src/app/services/notification.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';



@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})

export class LoginPageComponent implements OnInit {
  model: any = {};
  signInForm: FormGroup;


  constructor(private authService: CustomAuthService, private router: Router, fb: FormBuilder, private alertService: NotificationService)
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
      next => {
        console.log(this.model);
      },
      error => {
        this.alertService.errorMsg(error.message);
      },
      () => {
        this.authService.setLoggedIn(true);
        this.alertService.successMsg('Logged in successfuly');
        this.router.navigate(['user/profile']);
      });
  }

  googleSignIn() {
    this.authService.signInWithGoogle();
  }
  get f() { return this.signInForm.controls; }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
