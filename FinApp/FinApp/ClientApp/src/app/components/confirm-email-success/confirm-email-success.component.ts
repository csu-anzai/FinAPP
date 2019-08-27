import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmailConfirmationService } from '../../services/email-confirmation.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-confirm-email-success',
  templateUrl: './confirm-email-success.component.html',
  styleUrls: ['./confirm-email-success.component.css']
})

export class ConfirmEmailSuccessComponent implements OnInit {

  accessToken: string;
  private jwtHelper: JwtHelperService;

  constructor(private route: ActivatedRoute, private emailConfirmationService: EmailConfirmationService)
  {
    this.jwtHelper = new JwtHelperService();
    this.route.queryParams.subscribe(params => {
      this.accessToken = params['token'];
      console.log(this.accessToken);
    });
  }

  ngOnInit() {
    let decodedToken = this.jwtHelper.decodeToken(this.accessToken);
    let userId = decodedToken.sub;
    let model = { userId: userId, accessToken: this.accessToken };
    console.log(model);

    this.emailConfirmationService.validateConfirmEmailLink(model).subscribe(/*() => {}*/);
  }

}
