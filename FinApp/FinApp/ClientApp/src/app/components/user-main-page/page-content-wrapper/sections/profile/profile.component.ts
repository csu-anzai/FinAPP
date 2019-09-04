import { AuthService } from 'src/app/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { FormGroup, FormBuilder, Validators, FormControl, ValidatorFn, AbstractControl } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { NotificationService } from 'src/app/services/notification.service';
import { UploadService } from 'src/app/services/upload.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ErrorHandlingService } from 'src/app/services/error-handling.service';
import * as moment from 'moment';
import { format } from 'url';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  private _userAvatar: string;

  profileForm: FormGroup;
  user: User = new User();
  date: any;
  updateModal: any;
  files: File[] = [];


  public get Avatar(): string {
    if (this.user.avatar) {
      this._userAvatar = this.user.avatar;
      return this._userAvatar.includes('base64') ? this._userAvatar : `data:image/png;base64,${this._userAvatar}`;
    }

    return null;
  }

  public set Avatar(imgBase64: string) {
    this._userAvatar = imgBase64;
  }

  constructor(
    private _authService: AuthService,
    private _userService: UserService,
    private _uploadService: UploadService,
    private _modalService: NgbModal,
    private _errorHandler: ErrorHandlingService,
    fb: FormBuilder,
    private alertService: NotificationService) {

    this.profileForm = fb.group({
      'Name': new FormControl('', Validators.compose([Validators.required, Validators.pattern('[a-zA-z-]*')])),
      'BirthDate': new FormControl('', Validators.compose([Validators.required, Validators.pattern("^[0-9]{2}[\/][0-9]{2}[\/][0-9]{4}$")])), //^(\\d{2}|\\d)\/(\\d{2}|\\d)\/\\d{4}$"
      'Surname': new FormControl('', Validators.compose([Validators.required, Validators.pattern('[a-zA-z-]*')])),
      'Email': new FormControl('', Validators.compose([Validators.required, Validators.pattern("[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")]))
    });
  }

  ngOnInit() {
    this._userService.getUser(this._authService.DecodedToken.sub).subscribe(res => {
      console.log(res);
      this.user = res;
      this.profileForm.get('Name').setValue(this.user.name);
      this.profileForm.get('Surname').setValue(this.user.surname);
      this.profileForm.get('BirthDate').setValue(moment(this.user.birthDate).format('DD/MM/YYYY'));
      this.profileForm.get('Email').setValue(this.user.email);
    });
  }

  updateProfile() {
    if (this.profileForm.valid) {
      this.user.name = this.profileForm.controls['Name'].value,
      this.user.surname = this.profileForm.controls['Surname'].value,
      this.user.email = this.profileForm.controls['Email'].value,
      this.user.birthDate =  new Date((this.profileForm.controls['BirthDate'].value).split('/').reverse().join('-'));
      console.log(this.user.birthDate);
      
      this._userService.update(this.user).subscribe(() => {
        this.Avatar = this.user.avatar;
        this.alertService.successMsg('Profile updated');
      });
    }
    else {
      for (let i in this.profileForm.controls)
        this.profileForm.controls[i].markAsTouched();
    }
  }

  // drag&drop
  onUpdateAvatar(content) {
    this.updateModal = this._modalService.open(content, { centered: true });
  }

  closeModal() {
    // clear image after closing modal
    if (this.files.length) {
      this.files.shift();
    }
    this.updateModal.close();
  }

  // select kinda an event
  onSelect(event) {
    this.files.push(...event.addedFiles);
    if (this.files.length > 1) {
      this.files.shift();
    }
  }

  // remove from drap&drop
  onRemove(event) {
    this.files.splice(this.files.indexOf(event), 1);
  }

  // sent file into the service
  onSendImage() {
    this._uploadService.uploadUserAvatar(this.user.id, this.files[0]).toPromise()
    .then(
      (res: any) => {
        // validation error
        if (res.body) {
          if (res.body.error && res.body.code === 304) {
            this.alertService.waringMsg(res.body.error);
            return;
          }
        }
        this.ngOnInit();
        this.closeModal();
        this.alertService.successMsg('Image updated');
      }
    ).catch(
      err => this.alertService.errorMsg(err)
      );
  }

      // this._uploadService.uploadCategoryImage({ name: 'bbb.txt', path: 'ccccdfgdf' }, this.files[0]).toPromise()
    // .then(
    //   (res: any) => {
    //     // validation error
    //     if (res.body) {
    //       if (res.body.error && res.body.code === 304) {
    //         this.alertService.waringMsg(res.body.error);
    //         return;
    //       }
    //     }
    //     this.ngOnInit();
    //     this.closeModal();
    //     this.alertService.successMsg('Image updated');
    //   }
    // );

   dateValidator(): ValidatorFn {
    return (control: AbstractControl): {[key: string]: any} => {
      const dateStr = control.value;
      // Length of months (will update for leap years)
      const monthLengthArr = [ 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 ];
      // Object to return if date is invalid
      const invalidObj = { 'date': true };
      // Parse the date input to integers
      const dateArr = dateStr.split('/');
      const month = parseInt(dateArr[0], 10);
      const day = parseInt(dateArr[1], 10);
      const year = parseInt(dateArr[2], 10);
      // Today's date
      const now = new Date();
  
      // Validate year and month
      if (year < now.getFullYear() || year > 3000 || month === 0 || month > 12) {
        return invalidObj;
      }
      // Adjust for leap years
      if (year % 400 === 0 || (year % 100 !== 0 && year % 4 === 0)) {
        monthLengthArr[1] = 29;
      }
      // Validate day
      if (!(day > 0 && day <= monthLengthArr[month - 1])) {
        return invalidObj;
      }
      // If date is properly formatted, check the date vs today to ensure future
      // This is done this way to account for new Date() shifting invalid
      // date strings. This way we know the string is a correct date first.
      const date = new Date(dateStr);
      if (date <= now) {
        return invalidObj;
      }
      return null;
    };
  }
}
