import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { IncomeService } from '../../services/income.service';
import { CategoryService } from '../../services/category.service';
import { IncomeCategory } from '../../models/incomeCategory';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-add-income',
  templateUrl: './add-income.component.html',
  styleUrls: ['./add-income.component.css']
})
export class AddIncomeComponent implements OnInit {

  modal;
  incomeAddForm: FormGroup;
  categories: Observable<IncomeCategory[]>;

  constructor(private modalService: NgbModal,
    private incomeService: IncomeService,
    fb: FormBuilder,
    private categoryService: CategoryService) {
    this.incomeAddForm = fb.group({
      'Description': new FormControl(''),
      'Sum': new FormControl('', Validators.compose([Validators.required, Validators.min(0.01)])),
      'Date': new FormControl('', Validators.required),
      'IncomeCategory': new FormControl('', Validators.required)
    });
  }

  ngOnInit() {
    this.categories = this.categoryService.getCategories(true);
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
