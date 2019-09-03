import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ExpenseService } from '../../services/expense.service';
import { IncomeService } from '../../services/income.service';


@Component({
  selector: 'app-update-transaction',
  templateUrl: './update-transaction.component.html',
  styleUrls: ['./update-transaction.component.css']
})
export class UpdateTransactionComponent implements OnInit {

  isIncome: true;

  modal;
  expenseAddForm: FormGroup;

  constructor(private modalService: NgbModal,
    private expenseService: ExpenseService,
    private incomeService: IncomeService,

    fb: FormBuilder) {
    this.expenseAddForm = fb.group({
      'Description': new FormControl(''),
      'Sum': new FormControl('', Validators.compose([Validators.required, Validators.min(0.01)])),
      'Date': new FormControl('', Validators.required),
      'ExpenseCategory': new FormControl('', Validators.required)
    });
  }

  ngOnInit() {
  }

  openVerticallyCentered(content) {
    this.modal = this.modalService.open(content, { centered: true });
  }

  addExpense() {
    if (this.expenseAddForm.valid) {
      if (this.isIncome) {
        this.incomeService.updateIncome(this.expenseAddForm.value)
          .subscribe(data => {
            this.modal.close();
          });
      }
      else {
        this.expenseService.updateExpense(this.expenseAddForm.value)
          .subscribe(data => {
          this.modal.close();
        });
      }
    }
    else {
      for (let i in this.expenseAddForm.controls) {
        this.expenseAddForm.controls[i].markAsTouched();
      }
    }
  }

  closeModal() {
    this.modal.close();
    for (let i in this.expenseAddForm.controls)
      this.expenseAddForm.controls[i].markAsUntouched();
  }
}
