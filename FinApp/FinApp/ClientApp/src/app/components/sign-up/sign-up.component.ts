
import { Component, OnInit, AfterContentInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl, AbstractControl, ValidatorFn } from '@angular/forms';
import { CustomAuthService } from 'src/app/services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'angular-6-social-login';
@Component({
  selector: 'sign-up-component',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  signUpForm: FormGroup;
  user: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: CustomAuthService,
    private socialAuthService: AuthService,

    fb: FormBuilder) {
    this.signUpForm = fb.group({
      'Name': new FormControl('', Validators.compose([Validators.required, Validators.pattern('[a-zA-z-]*')])),
      'BirthDate': new FormControl('', Validators.required),
      'Surname': new FormControl('', Validators.compose([Validators.required, Validators.pattern('[a-zA-z-]*')])),
      'Email': new FormControl('', Validators.compose([Validators.required, Validators.pattern("[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")])),
      'Password': new FormControl('', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(16)])),
      'RepeatedPassword': new FormControl('', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(16)])),
    });
    this.signUpForm.valueChanges.subscribe(field => {
      if (this.signUpForm.controls['Password'].value !== this.signUpForm.controls['RepeatedPassword'].value) {
        this.signUpForm.controls['RepeatedPassword'].setErrors({ mismatch: true });
      } else {
        this.signUpForm.controls['RepeatedPassword'].setErrors(null);
      }
    });
  }

  ngOnInit(): void {
    // this.signUpForm.controls['Email'].setValue('');
    // this.signUpForm.controls['Name'].setValue('');
    // this.signUpForm.controls['Surname'].setValue('');

    this.socialAuthService.authState.subscribe(
      (user) => {
        if (user) {
          const fullName = user.name.split(' '),
            name = fullName[0],
            surname = fullName[fullName.length - 1];

          this.signUpForm.controls['Email'].setValue(user.email);
          this.signUpForm.controls['Name'].setValue(name);
          this.signUpForm.controls['Surname'].setValue(surname);
        }
      }
    );
  }

  onSignUp() {
    if (this.signUpForm.valid) {
      this.user = {
        Name: this.signUpForm.controls['Name'].value,
        Surname: this.signUpForm.controls['Surname'].value,
        BirthDate: this.signUpForm.controls['BirthDate'].value,
        Email: this.signUpForm.controls['Email'].value,
        Password: this.signUpForm.controls['Password'].value,
      };
      this.authService.register(this.user).subscribe(() => {
        // if email is not available -> show message
        this.router.navigate(['login-page']);
      });
    } else {
      console.log(
        this.signUpForm.controls['RepeatedPassword'].errors);
      for (let i in this.signUpForm.controls)
        this.signUpForm.controls[i].markAsTouched();
    }
  }
}
