import { UserService } from './../../../../../services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { first } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  user: User = new User();
  users: User[];


  constructor(private _authService: AuthService, private _userService: UserService) { }

  ngOnInit() {  
    this._userService.getUser(this._authService.DecodedToken.sub).subscribe(res => { this.user = res; });
    this._userService.getUsers().subscribe(data => {
      this.users=data;
    });
    // if(this.user instanceof User ){console.log("Yes")}
  }

  updateProfile() {
    
  }

  getUsers(){
    this._userService.getUsers().subscribe(data => {
      this.users=data;
    });
  }

  deleteUser(id:number) {
    console.log(id);
  //   this._userService.deleteUser(id).subscribe(data => {
  //     this.getUsers();
  //  });
  this._userService.deleteUser(id).subscribe(data => {
    this.getUsers();
 });
  }

}


