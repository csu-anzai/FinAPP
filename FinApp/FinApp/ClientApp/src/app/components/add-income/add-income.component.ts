import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { IncomeService } from '../../services/income.service';

@Component({
  selector: 'app-add-income',
  templateUrl: './add-income.component.html',
  styleUrls: ['./add-income.component.css']
})
export class AddIncomeComponent implements OnInit {

  modal;
  incomeAddForm: FormGroup;

  constructor(private modalService: NgbModal,
    private incomeService: IncomeService,
    fb: FormBuilder) {
    this.incomeAddForm = fb.group({
      'Description': new FormControl(''),
      'Sum': new FormControl('', Validators.compose([Validators.required, Validators.min(0.01)])),
      'Date': new FormControl('', Validators.required),
      'IncomeCategory': new FormControl('', Validators.required)
    });
  }

  ngOnInit() {
  }

  openVerticallyCentered(content) {
    this.modal = this.modalService.open(content, { centered: true });
  }

  addIncome() {
    if (this.incomeAddForm.valid) {

      this.incomeService.addIncome(this.incomeAddForm.value).subscribe(data => {
        this.modal.close();
      });
    }
    else {
      for (let i in this.incomeAddForm.controls)
        this.incomeAddForm.controls[i].markAsTouched();
    }
  }

  closeModal() {
    this.modal.close();
    for (let i in this.incomeAddForm.controls)
      this.incomeAddForm.controls[i].markAsUntouched();
  }
}
