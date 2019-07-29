import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;

  constructor(private authService: AuthService, private router: Router) { }

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
    localStorage.removeItem('token');
    this.router.navigate(['']);
  }
}
