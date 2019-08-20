import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css']
})
export class SettingComponent implements OnInit {

  constructor(private translateService: TranslateService) { }

  ngOnInit() {
  }

  switchLanguage(language: string) {
    this.translateService.use(language);
  }
}
