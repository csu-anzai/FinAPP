import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ForgotPasswordService } from '../../services/forgot.password.service';

@Component({
  selector: 'app-confirm-code',
  templateUrl: './confirm-code.component.html',
  styleUrls: ['./confirm-code.component.css']
})
export class ConfirmCodeComponent implements OnInit {
  confirmCodeForm: FormGroup;
  enteredCode;

  constructor(
    private router: Router,
    private forgotPasswordService: ForgotPasswordService,
    formBuilder: FormBuilder) {
    this.confirmCodeForm = formBuilder.group({
      'Code': new FormControl('', Validators.compose([Validators.required]))
    });
  }

  ngOnInit() {
  }

  onCodeSubmit() {
    if (this.confirmCodeForm.valid) {
      this.enteredCode = this.confirmCodeForm.controls['Code'].value,
       
      this.forgotPasswordService.validateCode(this.enteredCode).subscribe(() => {
        if (this.forgotPasswordService.enteredCodeIsValid == true) {
          this.router.navigate(['change-password']);
        }
      });
    }
    else {
      for (let i in this.confirmCodeForm.controls)
        this.confirmCodeForm.controls[i].markAsTouched();
    }
  }
}
