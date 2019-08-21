import { AuthService } from 'src/app/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { NotificationService } from 'src/app/services/notification.service';
import { OAuthService } from 'angular-oauth2-oidc';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { share, map } from 'rxjs/operators';



@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  jwtHelper = new JwtHelperService();
  isExpanded = false;

  constructor(private authService: AuthService,
    private cookieService: CookieService,
    private router: Router,
    private oauthService: OAuthService,
    private alertService: NotificationService) { }

  ngOnInit() {
    this.loggedIn();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  onLogout() {
    console.log('onLogout()');
    this.authService.setLoggedIn(false);
    // this.oauthService.logOut(true);
    this.cookieService.delete('token', '/');
    this.router.navigate(['']);
    this.alertService.infoMsg(this.alertService.logOutMessage);
  }
}
