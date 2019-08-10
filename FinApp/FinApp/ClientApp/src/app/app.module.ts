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
import { CustomAuthService } from './services/auth.service';
import { LeftSideBarComponent } from './components/user-main-page/left-side-bar/left-side-bar.component';
import { GuestGuard } from './guest.guard';
import { ProfileComponent } from './components/user-main-page/page-content-wrapper/sections/profile/profile.component';
import { PageContentWrapperComponent } from './components/user-main-page/page-content-wrapper/page-content-wrapper.component';
import { AccountComponent } from './components/user-main-page/page-content-wrapper/sections/account/account.component';
import { SettingComponent } from './components/user-main-page/page-content-wrapper/sections/setting/setting.component';
import { ChartComponent } from './components/user-main-page/page-content-wrapper/sections/chart/chart.component';
import { NotificationService } from './services/notification.service';
import { MainPageComponent } from './landing-page/main-page/main-page.component';
import { DataService } from './common/data.service';
import { OAuthModule } from 'angular-oauth2-oidc';

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
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    OAuthModule.forRoot(),
    BsDatepickerModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: MainPageComponent, pathMatch: 'full' },
      { path: 'login-page', component: LoginPageComponent, canActivate: [GuestGuard] },
      { path: 'counter', component: CounterComponent },
      { path: 'sign-up', component: SignUpComponent, canActivate: [GuestGuard] },
      {
        path: 'user',
        component: FetchDataComponent,
        canActivate: [AuthGuard],
        children: [
          { path: 'profile', component: ProfileComponent },
          { path: 'charts', component: ChartComponent },
          { path: 'accounts', component: AccountComponent },
          { path: 'settings', component: SettingComponent }
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
    CustomAuthService,
    CookieService,
    NotificationService,
    GuestGuard,
    AuthGuard,
    DataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
