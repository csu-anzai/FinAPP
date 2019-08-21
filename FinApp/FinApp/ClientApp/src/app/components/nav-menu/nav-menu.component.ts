import { AuthService } from 'src/app/services/auth.service';
import { Component, OnInit, OnChanges } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { NotificationService } from 'src/app/services/notification.service';
import { OAuthService } from 'angular-oauth2-oidc';
import { JwtHelperService } from '@auth0/angular-jwt/src/jwthelper.service';
import { share, map } from 'rxjs/operators';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';



@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  private logOutMsg: string;
  jwtHelper = new JwtHelperService();
  isExpanded = false;

  constructor(private authService: AuthService,
    private cookieService: CookieService,
    private router: Router,
    private alertService: NotificationService,
    private translate: TranslateService) { }

  ngOnInit() {
    this.loggedIn();

    this.translate.onLangChange.subscribe((event: LangChangeEvent) => {
          this.translate.get('notifications.logOutMsg').subscribe(
        (text: string) => this.logOutMsg = text
      );
    });

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
    this.alertService.infoMsg(this.logOutMsg);
  }
}
