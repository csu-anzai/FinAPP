import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { SignUpUser } from './signUpUser';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'sign-up-component',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class  SignUpComponent implements OnInit {

  signUpForm: FormGroup;

  constructor(
    private router: Router,
    private authService: AuthService
  ) {
    // redirect to home if already logged in
    //if (this.authService.currentUserValue) {
    //  this.router.navigate(['/']);
    //}
  }

  user = new SignUpUser('', '', '', '');

  ngOnInit() {
  }

  onSignUp() {
    this.authService.register(this.user).subscribe(() => {
      this.router.navigate(['login-page']);
    });
  }


  // convenience getter for easy access to form fields
  get f() { return this.signUpForm.controls; }

  registrationByGoogle() {
    alert("Google");
  }

}
