import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { WelcomePageComponent } from './welcome-components/welcome-page/welcome-page.component';
import { WelcomeHeaderComponent } from './welcome-components/welcome-header/welcome-header.component';
import { WelcomeListComponent } from './welcome-components/welcome-list/welcome-list.component';
import { WelcomeBenefitsComponent } from './welcome-components/welcome-benefits/welcome-benefits.component';
import { WelcomeCarouselComponent } from './welcome-components/welcome-carousel/welcome-carousel.component';

import { AuthService } from './_services/auth.service';
import { NotificationService } from './_services/notification.service';
import { GlobalErrorHandler } from './common/global-error-handler';
import { ErrorService } from './_services/error.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    WelcomePageComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginPageComponent,
    SignUpComponent,
    WelcomeListComponent,
    WelcomeHeaderComponent,
    WelcomeBenefitsComponent,
    WelcomeCarouselComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: WelcomePageComponent, pathMatch: 'full' },
      { path: 'login-page', component: LoginPageComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'sign-up', component: SignUpComponent },
    ]),
    BrowserAnimationsModule,
    ToastrModule.forRoot(
      {
        positionClass: 'toast-bottom-right',
        autoDismiss: true,
        maxOpened: 5,
        tapToDismiss: true,
        timeOut: 3000,
        onActivateTick: true
      }
    )
  ],
  providers: [
    AuthService,
    CookieService,
    NotificationService,
    ErrorService,
    { provide: ErrorHandler, useClass: GlobalErrorHandler },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
