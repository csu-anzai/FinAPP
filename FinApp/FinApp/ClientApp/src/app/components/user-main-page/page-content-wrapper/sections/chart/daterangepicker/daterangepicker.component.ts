import { Component, OnInit } from '@angular/core';
import { BsDaterangepickerConfig } from 'ngx-bootstrap/datepicker/public_api';
import * as moment from 'moment';
import { VALUES_TO_SELECT } from './daterangepicker.constants';



@Component({
  selector: 'app-daterangepicker',
  templateUrl: './daterangepicker.component.html',
  styleUrls: ['./daterangepicker.component.css']
})
export class DaterangepickerComponent implements OnInit {
  datePickerConfig:Partial<BsDaterangepickerConfig>;
  dataRanges: string[];
  selectedDates;
  constructor() 
  { 
    this.datePickerConfig = Object.assign({},{containerClass: 'theme-dark-blue'});
    this.dataRanges = Object.keys(VALUES_TO_SELECT).map(key => VALUES_TO_SELECT[key]);
    this.selectedDates = [moment().toDate(),moment().toDate()];
  }
  onChange(value){
    switch (value) {
      case VALUES_TO_SELECT.TODAY:
        const today = moment().toDate();
        this.selectedDates = [today, today];
        break;
      case VALUES_TO_SELECT.YESTERDAY:
        const yesterday = moment().subtract(1,'day').toDate();
        this.selectedDates = [yesterday, yesterday];
        break;
      case VALUES_TO_SELECT.LAST_WEEK:
        const last_week = moment().subtract(7,'day').toDate();
        this.selectedDates = [last_week,moment().toDate()];
        break;
      case VALUES_TO_SELECT.LAST_MONTH:
        const last_month = moment().subtract(1,'month').toDate();
        this.selectedDates = [last_month,moment().toDate()];
        break;
      case VALUES_TO_SELECT.LAST_YEAR:
        const last_year = moment().subtract(1,'year').toDate();
        this.selectedDates = [last_year,moment().toDate()];
        break;  
            
    }

  }
  
  ngOnInit() {
  }
 

}
