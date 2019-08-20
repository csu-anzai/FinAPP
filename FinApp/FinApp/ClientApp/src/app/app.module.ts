// Modules
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { CookieService } from 'ngx-cookie-service';
import { FetchDataComponent } from './components/user-main-page/user-main-page';
import { AuthGuard } from './auth.guard';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OAuthModule } from 'angular-oauth2-oidc';
import { ChartsModule } from 'ng2-charts/ng2-charts';

// Components
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { WelcomePageComponent } from './components/welcome-components/welcome-page/welcome-page.component';
import { WelcomeHeaderComponent } from './components/welcome-components/welcome-header/welcome-header.component';
import { WelcomeListComponent } from './components/welcome-components/welcome-list/welcome-list.component';
import { WelcomeBenefitsComponent } from './components/welcome-components/welcome-benefits/welcome-benefits.component';
import { WelcomeCarouselComponent } from './components/welcome-components/welcome-carousel/welcome-carousel.component';
import { LeftSideBarComponent } from './components/user-main-page/left-side-bar/left-side-bar.component';
import { ProfileComponent } from './components/user-main-page/page-content-wrapper/sections/profile/profile.component';
import { PageContentWrapperComponent } from './components/user-main-page/page-content-wrapper/page-content-wrapper.component';
import { AccountComponent } from './components/user-main-page/page-content-wrapper/sections/account/account.component';
import { SettingComponent } from './components/user-main-page/page-content-wrapper/sections/setting/setting.component';
import { DaterangepickerComponent } from './components/user-main-page/page-content-wrapper/sections/chart/daterangepicker/daterangepicker.component';
import { ChartsComponent } from './components/user-main-page/page-content-wrapper/sections/chart/charts/charts.component';
import { ChartComponent } from './components/user-main-page/page-content-wrapper/sections/chart/chart.component';

import { FilterPipe } from './components/user-main-page/page-content-wrapper/sections/account/account-history/filter.pipe';

import { MainPageComponent } from './landing-page/main-page/main-page.component';

import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { AccountHistoryComponent } from './components/user-main-page/page-content-wrapper/sections/account/account-history/account-history.component';
import { AccountInfoComponent } from './components/user-main-page/page-content-wrapper/sections/account/account-info/account-info.component';
import { AddAccountComponent } from './components/user-main-page/page-content-wrapper/sections/account/add-account/add-account.component';

// Services
import { AuthService } from './services/auth.service';
import { GuestGuard } from './guest.guard';
import { NotificationService } from './services/notification.service';
import { MessagingCenterService } from './services/messaging-center.service';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { UserService } from './services/user.service';
import { ForgotPasswordService } from './services/forgot.password.service';
import { ConfirmCodeComponent } from './components/confirm-code/confirm-code.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';

import { JwtInterceptor } from './interceptors/jwt-interceptor';
import { ChartsService} from './services/charts.service';

import { FusionChartsModule } from 'angular-fusioncharts';
import * as FusionCharts from 'fusioncharts';
import * as Charts from 'fusioncharts/fusioncharts.charts';
import * as FusionTheme from 'fusioncharts/themes/fusioncharts.theme.fusion';

FusionChartsModule.fcRoot(FusionCharts, Charts, FusionTheme);

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
    MainPageComponent,
    LeftSideBarComponent,
    PageContentWrapperComponent,
    ProfileComponent,
    ChartComponent,
    AccountComponent,
    SettingComponent,
    AccountHistoryComponent,
    AccountInfoComponent,
    AdminPanelComponent,
    AddAccountComponent,
    FilterPipe,
    ForgotPasswordComponent,
    ConfirmCodeComponent,
    ChangePasswordComponent,
    DaterangepickerComponent,
    ChartsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    FusionChartsModule,
    ChartsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    OAuthModule.forRoot(),
    BsDatepickerModule.forRoot(),
    NgbModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: MainPageComponent, pathMatch: 'full' },
      { path: 'login-page', component: LoginPageComponent, canActivate: [GuestGuard] },
      { path: 'counter', component: CounterComponent },
      { path: 'sign-up', component: SignUpComponent, canActivate: [GuestGuard] },
      { path: 'forgot-password', component: ForgotPasswordComponent, canActivate: [GuestGuard] },
      { path: 'confirm-code', component: ConfirmCodeComponent, canActivate: [GuestGuard] },
      { path: 'change-password', component: ChangePasswordComponent, canActivate: [GuestGuard] },
      {
        path: 'user',
        component: FetchDataComponent,
        canActivate: [AuthGuard],
        children: [
          { path: 'profile', component: ProfileComponent },
          { path: 'charts', component: ChartComponent },
          { path: 'accounts', component: AccountComponent },
          { path: 'settings', component: SettingComponent },
          { path: 'accounts/:id', component: AccountComponent },
          { path: 'settings', component: SettingComponent },
          { path: 'adminPanel', component: AdminPanelComponent }
        ]
      }
    ]),
    BrowserAnimationsModule,
    ToastrModule.forRoot(
      {
        positionClass: 'toast-bottom-right',
        closeButton: true,
        timeOut: 3000,
        easeTime: 1000,
        maxOpened: 5
      }
    )
  ],
  providers: [
    AuthService,
    CookieService,
    NotificationService,
    GuestGuard,
    AuthGuard,
    MessagingCenterService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    UserService,
      ForgotPasswordService,
      ChartsService,

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
