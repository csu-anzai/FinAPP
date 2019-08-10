import { Component, OnInit } from '@angular/core';
import { Account } from '../../../../../../models/account';
import { Currency } from '../../../../../../models/currency';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {

  account = new Account(1, 'PrivatAcc', new Currency(1, '$', 2.5), '1.jpg', 2000);

  constructor() { }

  ngOnInit() {
  }

}
