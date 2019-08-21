import { Component, OnInit } from '@angular/core';
import { LanguageService } from 'src/app/services/language.service';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css']
})
export class SettingComponent implements OnInit {

  constructor(private language: LanguageService) { }

  ngOnInit() {
  }

  switchLanguage(language: string) {
    this.language.switchLanguage(language);
  }
}
