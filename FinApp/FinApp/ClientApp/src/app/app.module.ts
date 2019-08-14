import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { CookieService } from 'ngx-cookie-service';
import { FetchDataComponent } from './components/user-main-page/user-main-page';
import { AuthGuard } from './auth.guard';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
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
import { AuthService } from './services/auth.service';
import { LeftSideBarComponent } from './components/user-main-page/left-side-bar/left-side-bar.component';
import { GuestGuard } from './guest.guard';
import { ProfileComponent } from './components/user-main-page/page-content-wrapper/sections/profile/profile.component';
import { PageContentWrapperComponent } from './components/user-main-page/page-content-wrapper/page-content-wrapper.component';
import { AccountComponent } from './components/user-main-page/page-content-wrapper/sections/account/account.component';
import { SettingComponent } from './components/user-main-page/page-content-wrapper/sections/setting/setting.component';
import { ChartComponent } from './components/user-main-page/page-content-wrapper/sections/chart/chart.component';
import { NotificationService } from './services/notification.service';


import { MainPageComponent } from './landing-page/main-page/main-page.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { AccountHistoryComponent } from './components/user-main-page/page-content-wrapper/sections/account/account-history/account-history.component';
import { AccountInfoComponent } from './components/user-main-page/page-content-wrapper/sections/account/account-info/account-info.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { UserService } from './services/user.service';
import { ForgotPasswordService } from './services/forgot.password.service';
import { ConfirmCodeComponent } from './components/confirm-code/confirm-code.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';

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
    ForgotPasswordComponent,
    ConfirmCodeComponent,
    ChangePasswordComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
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
      }
    )
  ],
  providers: [
    AuthService,
    CookieService,
    NotificationService,
    GuestGuard,
    AuthGuard,
    UserService,
    ForgotPasswordService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
