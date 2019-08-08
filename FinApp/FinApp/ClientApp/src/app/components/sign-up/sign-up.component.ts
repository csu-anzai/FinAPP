
import { Component, OnInit, AfterContentInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl, AbstractControl, ValidatorFn } from '@angular/forms';
import { CustomAuthService } from 'src/app/services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { DataService } from 'src/app/common/data.service';
@Component({
  selector: 'sign-up-component',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  signUpForm: FormGroup;
  user: any = {
    Name: '',
    Surname: '',
    BirthDate: '',
    Email: '',
    Password: '',
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: CustomAuthService,
    private data: DataService,

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
      console.log(this.user.Password + '\t' + this.signUpForm.controls['RepeatedPassword'].value);
      if (this.user.Password != this.signUpForm.controls['RepeatedPassword'].value) {
        this.signUpForm.controls['RepeatedPassword'].setErrors({ mismatch: true });
      } else {
        this.signUpForm.controls['RepeatedPassword'].setErrors(null);
      }
    });
  }

  ngOnInit(): void {
    this.data.currentParams.subscribe((p: any) => { this.user.Email = p.email; this.user.Name = p.name; });
  }

  onSignUp() {
    if (this.signUpForm.valid) {
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
