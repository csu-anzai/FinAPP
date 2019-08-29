import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmailConfirmationService } from '../../services/email-confirmation.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-confirm-email-success',
  templateUrl: './confirm-email-success.component.html',
  styleUrls: ['./confirm-email-success.component.css']
})

export class ConfirmEmailSuccessComponent implements OnInit {

  accessToken: string;
  messageToUser: string;
  private jwtHelper: JwtHelperService;

  constructor(private activatedRoute: ActivatedRoute, private emailConfirmationService: EmailConfirmationService, private router: Router, private alertService: NotificationService)
  {
    this.jwtHelper = new JwtHelperService();
    this.activatedRoute.queryParams.subscribe(params => {
      this.accessToken = params['token'];
    });
  }

  ngOnInit() {
    let decodedToken = this.jwtHelper.decodeToken(this.accessToken);
    let userId = decodedToken.sub;
    let model = { userId: userId, accessToken: this.accessToken };

    this.emailConfirmationService.validateConfirmEmailLink(model).subscribe(
      next => { },

      error => {
        if (error.status == 403) //this.router.navigate(['sign-up']);
      },
      () => { }
    );
  }

  signIn() {
    this.router.navigate(['login-page']);
  }
}
