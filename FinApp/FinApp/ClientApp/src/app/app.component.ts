import { Component } from '@angular/core';
import { OAuthService, JwksValidationHandler  } from 'angular-oauth2-oidc';
import { authCodeFlowConfig } from './configs/auth-code-flow.config';
import { filter } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';
import { TranslateService } from '@ngx-translate/core';
import { LanguageService } from './services/language.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  constructor(private oauthService: OAuthService,
    private authService: AuthService,
    private language: LanguageService) {
    this.language.languageSetUp();
    this.configure();
    
    // Receives a response from google oauth2
    this.oauthService.events
    .pipe(filter(e => e.type === 'token_received'))
    .subscribe(_ => {
      console.log(this.oauthService.getIdToken());
      this.authService.getDataFromTokenId(this.oauthService.getIdToken());
    });
  }

  configure() {
    this.oauthService.configure(authCodeFlowConfig);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
    this.oauthService.setupAutomaticSilentRefresh();
  }
}
