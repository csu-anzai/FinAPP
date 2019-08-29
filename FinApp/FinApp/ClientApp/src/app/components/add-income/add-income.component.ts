import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-add-income',
  templateUrl: './add-income.component.html',
  styleUrls: ['./add-income.component.css']
})
export class AddIncomeComponent implements OnInit {
  modal;
  incomeAddForm: FormGroup;
  constructor(private modalService: NgbModal) { }

  ngOnInit() {
  }

  closeModal() {
    this.modal.close();
    for (let i in this.incomeAddForm.controls)
      this.incomeAddForm.controls[i].markAsUntouched();
  }

  openVerticallyCentered(content) {

    this.modal = this.modalService.open(content, { centered: true });
  }
}
