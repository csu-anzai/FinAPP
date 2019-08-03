import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'sign-up-component',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  signUpForm: FormGroup;
  user;

  constructor(
    private router: Router,
    private authService: AuthService,
    fb: FormBuilder
  ) {
    this.signUpForm = fb.group({
      'Name': new FormControl('', Validators.compose([Validators.required, Validators.pattern('[a-zA-z]*')])),
      'BirthDate': new FormControl('', Validators.required),
      'Surname': new FormControl('', Validators.compose([Validators.required, Validators.pattern('[a-zA-z]*')])),
      'Email': new FormControl('', Validators.compose([Validators.required, Validators.email])),
      'Password': new FormControl('', Validators.required),
      'RepeatedPassword': new FormControl(''),
    });

  }

  ngOnInit() {
  }

  onSignUp() {
    if (this.signUpForm.valid && this.signUpForm.controls['Password'].value === this.signUpForm.controls['RepeatedPassword'].value) {
      this.user = {
        Name: this.signUpForm.controls['Name'].value,
        Surname: this.signUpForm.controls['Surname'].value,
        BirthDate: this.signUpForm.controls['BirthDate'].value,
        Email: this.signUpForm.controls['Email'].value,
        Password: this.signUpForm.controls['Password'].value,
      };
      this.authService.register(this.user).subscribe(
        () => {
          this.router.navigate(['login-page']);
        });
    } else {
      // tslint:disable-next-line: forin
      for (const i in this.signUpForm.controls) {
        this.signUpForm.controls[i].markAsTouched();
      }
    }
  }


  // convenience getter for easy access to form fields
  get f() { return this.signUpForm.controls; }

  registrationByGoogle() {
    alert('Google');
  }
}
