import { AuthService } from 'src/app/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { NotificationService } from 'src/app/services/notification.service';
import  * as moment from 'moment';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  profileForm: FormGroup;
  user: User = new User();
  date: any;
  constructor(
    private _authService: AuthService,
    private _userService: UserService,
    fb: FormBuilder,
    private alertService: NotificationService) {

    this.profileForm = fb.group({
      'Name': new FormControl('', Validators.compose([Validators.required, Validators.pattern('[a-zA-z-]*')])),
      'BirthDate': new FormControl('', Validators.compose([Validators.required])),
      'Surname': new FormControl('', Validators.compose([Validators.required, Validators.pattern('[a-zA-z-]*')])),
      'Email': new FormControl('', Validators.compose([Validators.required, Validators.pattern("[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")]))
    });
  }

  ngOnInit() {
    this._userService.getUser(this._authService.DecodedToken.sub).subscribe(res => {
      //console.log(res)
      this.user = res;

      this.profileForm.get('Name').setValue(this.user.name);
      this.profileForm.get('Surname').setValue(this.user.surname);
    //  this.profileForm.get('BirthDate').setValue(this.user.birthDate);
      this.date = this.user.birthDate.toString().split('-').reverse().join('-');
      this.profileForm.get('BirthDate').setValue(this.date);
      this.profileForm.get('Email').setValue(this.user.email);
    });
  }

  updateProfile() {
    if (this.profileForm.valid) {
      this.user.name = this.profileForm.controls['Name'].value,
        this.user.surname = this.profileForm.controls['Surname'].value,
        this.user.email = this.profileForm.controls['Email'].value,
       // this.user.birthDate = new Date(moment(this.profileForm.controls['BirthDate'].value,'DD/MM/YYYY').format('MM/DD/YYYY'));
        this.user.birthDate = new Date((this.profileForm.controls['BirthDate'].value).split('-').reverse().join('-'));
        this._userService.update(this.user).subscribe(() => {
          console.log(this.user);
          this.alertService.successMsg('Profile updated');
        });
    }
    else {
      for (let i in this.profileForm.controls)
        this.profileForm.controls[i].markAsTouched();
    }
  }
}

// getUsers(){
  //   this._userService.getUsers().subscribe(data => {
  //     this.users=data;
  //   });
  // }

//   deleteUser(id:number) {
//     console.log(id);
//   //   this._userService.deleteUser(id).subscribe(data => {
//   //     this.getUsers();
//   //  });
//   this._userService.deleteUser(id).subscribe(data => {
//     this.getUsers();
//  });
//   }

// this.user.birthDate
    //   this.date = new Date(moment(this.profileForm.controls['BirthDate'].value,'DD/MM/YYYY').format('MM/DD/YYYY'));
    //   console.log(this.date);
      //   this.date = new Date(moment(this.profileForm.controls['BirthDate'].value).format("MM/dd/YYYY")),
