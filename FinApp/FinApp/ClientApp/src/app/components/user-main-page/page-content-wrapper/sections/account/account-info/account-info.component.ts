import { Component, OnInit, Input } from '@angular/core';
import { Account } from '../../../../../../models/account';
import { AccountService } from '../../../../../../services/account.service'
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {

  account: Account = new Account();
  id: number;

  constructor(public accountService: AccountService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];

      this.accountService.getAccount(this.id).subscribe(data => {
        this.account = data;
      });
    });

  }



}
