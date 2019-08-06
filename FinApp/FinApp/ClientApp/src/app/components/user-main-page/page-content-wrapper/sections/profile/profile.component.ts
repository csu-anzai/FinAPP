import { UserService } from './../../../../../services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
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
    this._userService.getById(this._authService.DecodedToken.sub).subscribe(res => { this.user = res; });
    // if(this.user instanceof User ){console.log("Yes")}
  }

}
