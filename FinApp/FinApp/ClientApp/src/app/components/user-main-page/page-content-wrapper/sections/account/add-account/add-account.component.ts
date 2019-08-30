import { Component, OnInit } from '@angular/core';
import { Currency } from '../../../../../../models/currency';
import { Image } from '../../../../../../models/image';
import { AccountAdd } from '../../../../../../models/accountAdd';
import { CurrencyService } from '../../../../../../services/currency.service';
import { ImageService } from '../../../../../../services/image.service';
import { AccountService } from '../../../../../../services/account.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.css']
})
export class AddAccountComponent implements OnInit {

  images: Image[];

  currencies: Currency[];

  accountAdd: AccountAdd = new AccountAdd();

  accountAddForm: FormGroup;
  

  constructor(private currencyService: CurrencyService,
    private imageService: ImageService,
    private accountService: AccountService,
    fb: FormBuilder) {
    this.accountAddForm = fb.group({
      'Name': new FormControl(this.accountAdd.name, Validators.required),
      'Balance': new FormControl(this.accountAdd.balance, Validators.compose([Validators.required, Validators.min(0), Validators.pattern('^[0-9]*[.,]?[0-9]+$')])),
      'Image': new FormControl('', Validators.required),
      'Currency': new FormControl('', Validators.required),
    });
  }


  ngOnInit() {
    this.currencyService.getAll()
      .subscribe(data => {
        this.currencies = data;
      });

    this.imageService.getAll()
      .subscribe(data => {
        this.images = data;
      });
  }
  

  addAccount() {
    if (this.accountAddForm.valid) {
      this.accountAdd.imageId = this.accountAddForm.controls['Image'].value;
      this.accountAdd.currencyId = this.accountAddForm.controls['Currency'].value;
      this.accountAdd.balance = this.accountAddForm.controls['Balance'].value;
      this.accountAdd.name = this.accountAddForm.controls['Name'].value;

      this.accountService.addAccount(this.accountAdd).subscribe(data => {
      });
    }
    else {
      for (let i in this.accountAddForm.controls)
        this.accountAddForm.controls[i].markAsTouched();
    }
  }
  
}
