import { Component, OnInit } from '@angular/core';
import { Currency } from '../../../../../../models/currency';
import { Image } from '../../../../../../models/image';
import { AccountAdd } from '../../../../../../models/accountAdd';
import { CurrencyService } from '../../../../../../services/currency.service';
import { ImageService } from '../../../../../../services/image.service';
import { AccountService } from '../../../../../../services/account.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators, FormControl, AbstractControl, ValidatorFn } from '@angular/forms';

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

  modal;

  constructor(private currencyService: CurrencyService,
    private imageService: ImageService,
    private accountService: AccountService,
    private modalService: NgbModal,
    fb: FormBuilder) {
    this.accountAddForm = fb.group({
      'Name': new FormControl(this.accountAdd.name, Validators.required),
      'Balance': new FormControl(this.accountAdd.balance, Validators.compose([Validators.required, Validators.min(0)])),
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

  openVerticallyCentered(content) {
    console.log(this.images);

    console.log(this.currencies);

   this.modal = this.modalService.open(content, { centered: true });
  }

  addAccount() {
    this.accountAdd.imageId = this.accountAddForm.controls['Image'].value;

    this.accountAdd.currencyId = this.accountAddForm.controls['Currency'].value;

    this.accountService.addAccount(this.accountAdd).subscribe(data => {

      //handling errors in future
      this.modal.close();
    });
  }

}
