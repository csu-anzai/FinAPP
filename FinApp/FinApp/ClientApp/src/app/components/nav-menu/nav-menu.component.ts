import { CustomAuthService } from 'src/app/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';import { NotificationService } from 'src/app/services/notification.service';



@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  
  constructor(private authService: CustomAuthService,
    private cookieService: CookieService,
    private router: Router,
    private alertService: NotificationService) {  }

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
    this.authService.setLoggedIn(false);
    this.cookieService.delete('token');
    this.router.navigate(['']);
    this.alertService.infoMsg('Logged Out');
  }
}
