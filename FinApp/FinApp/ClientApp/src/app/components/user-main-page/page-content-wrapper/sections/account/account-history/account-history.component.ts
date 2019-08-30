import { Component, OnInit } from '@angular/core';
import { FilterPipe } from './filter.pipe'

@Component({
  selector: 'app-account-history',
  templateUrl: './account-history.component.html',
  styleUrls: ['./account-history.component.css']
})
export class AccountHistoryComponent implements OnInit {
  transactions = [];
  constructor() { }

  ngOnInit() {
    this.transactions = [
      { IncomeOrExpense: 'Income', NameCategory: 'Salary', Path:'' , NameImg: "1.jpg", Description: 'Bla lavasa hoho kina hiker jino', Sum: '2000', Date: '12/04/1999' },
      { IncomeOrExpense: 'Expense', NameCategory: 'Food', Path: '', NameImg: "1.jpg", Description: 'la lavasa hoho kina hiker jino', Sum: '2000', Date: '12/04/1999' },
    ];
  }

  sort() { }
}
