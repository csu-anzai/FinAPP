import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-left-side-bar',
  templateUrl: './left-side-bar.component.html',
  styleUrls: ['./left-side-bar.component.css']
})
export class LeftSideBarComponent implements OnInit {

  accounts;

  constructor(public authService: AuthService,
    private userService: UserService) {
  }

  ngOnInit() {
    this.userService.getUser(this.authService.DecodedToken.sub).subscribe(data => {
      this.accounts = data.accounts;
      this.accounts.length();
    });
  }

}


// import { Component, OnInit } from '@angular/core';

// const LINKS = [
//   { path: "/user/profile", icon: "fa fa-user-circle", text: "Profile" },
//   { path: "/user/charts", icon: "fa fa-area-chart", text: "Charts" },
//   { path: "/user/accounts", icon: "fa fa-area-chart", text: "Accounts", sublinks: [
//     {}
//   ] },
//   { path: "/user/settings", icon: "fa fa-cogs", text: "Settings" },
//   { path: "/user/settings", icon: "fa fa-question-circle", text: "FAQ" },
// ];

// @Component({
//   selector: 'app-left-side-bar',
//   templateUrl: './left-side-bar.component.html',
//   styleUrls: ['./left-side-bar.component.css']
// })
// export class LeftSideBarComponent implements OnInit {
//   links = [];
//   constructor() {
//     this.links = LINKS;
//   }

//   ngOnInit() {
//   }

// }

