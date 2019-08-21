import { Injectable, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { CookieService } from 'ngx-cookie-service';
import { lang } from 'moment';

@Injectable({
  providedIn: 'root'
})
export class LanguageService implements OnInit {

  constructor(private translate: TranslateService,
    private cookieService: CookieService) { }

  ngOnInit() {
    // this.languageSetUp();
  }

  switchLanguage(language: string) {
    this.translate.use(language);
    this.renewLanguage(language);
  }

  renewLanguage(language: string) {
    this.cookieService.delete('language');
    this.cookieService.set('language', language, null, '/', null, true);
  }

  languageSetUp() {
    this.translate.setDefaultLang('en');
    const isSelectedInCookies = this.isLanguageWasSelected();
    if (!isSelectedInCookies) {
      const defaultLanguage = this.translate.getDefaultLang();
      this.translate.use(defaultLanguage);
    }
  }

  isLanguageWasSelected() {
    const isSelected = this.cookieService.check('language');
    if (isSelected) {
      const getLanguage = this.cookieService.get('language');
      this.translate.use(getLanguage);
      return isSelected;
    }
    return !isSelected;
  }
}
