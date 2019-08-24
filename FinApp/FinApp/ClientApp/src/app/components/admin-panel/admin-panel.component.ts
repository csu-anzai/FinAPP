import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/category';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { NotificationService } from '../../services/notification.service';

import * as FusionCharts from 'fusioncharts';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css'],
  providers: [UserService, CategoryService]
})
export class AdminPanelComponent implements OnInit {

  categoryForm: FormGroup;
  category: Category = new Category();

  expenseCategories: Category[];
  incomeCategories: Category[];
  users: User[];

  ready: boolean;

  dataSource: Object;
  chartConfig: Object;

  constructor(private categoryService: CategoryService, public userService: UserService, fb: FormBuilder, private alertService: NotificationService) {
    this.chartConfig = {
      width: '700',
      height: '400',
      type: 'column3d',
      dataFormat: 'json',
    };

    this.dataSource = {
      "chart": {
        "caption": "Title",
        "subCaption": "SubTitle",
        "xAxisName": "Month",
        "yAxisName": "Cash ($)",
        "numberSuffix": "K",
        "theme": "fusion",
      },
      "data": [{
        "label": "Venezuela",
        "value": "290"
      }, {
        "label": "Saudi",
        "value": "260"
      }, {
        "label": "Canada",
        "value": "180"
      }, {
        "label": "Iran",
        "value": "140"
      }, {
        "label": "Russia",
        "value": "115"
      }, {
        "label": "UAE",
        "value": "100"
      }, {
        "label": "US",
        "value": "30"
      }, {
        "label": "China",
        "value": "30"
      }]
    };

    this.categoryForm = fb.group({
      'Name': new FormControl('', Validators.compose([Validators.required, Validators.pattern('[a-zA-z-]*')])),
    });
  }

  ngOnInit() {
    this.doWork();
  }


  async doWork() {
    // this._categoryService = await this.getJsonResult("user", "getUsers").toPromise();
    this.loadUsers();
    this.loadCategories();

    this.ready = true;
  }

  async loadUsers() {
    this.users = await this.userService.getUsers().toPromise();
  }

  async loadCategories() {
    this.expenseCategories = await this.categoryService.getCategories(false).toPromise();
    this.incomeCategories = await this.categoryService.getCategories(true).toPromise();
  }

  addExpenseCategory() {
    this.categoryService.creationExpenseCategory(this.category, false).subscribe(() => {
      console.log(this.category);
      this.loadCategories();
      this.alertService.successMsg('Category added');
    });
  }
  addIncomeCategory() {
    if (!this.category.name) {
      this.alertService.errorMsg('Category doesn`t have a value');
      return;
    }

    this.categoryService.creationIncomeCategory(this.category, true).subscribe(() => {
      console.log(this.category);
      this.loadCategories();
      this.alertService.successMsg('Category added');
    });
  }

  editCategory(category: Category, isIncomeCategory: boolean) {
    this.categoryService.updateCategory(category, isIncomeCategory).subscribe(() => {
      this.loadCategories();
      this.alertService.successMsg('Category was updated');
    });
  }

  deleteUser(id: number) {
    this.userService.deleteUser(id).subscribe(() => {
      this.loadUsers();
      this.alertService.successMsg('Users updated');
    });
  }

  deleteCategory(id: number, isIncomeCategory: boolean) {
    this.categoryService.deleteCategory(id, isIncomeCategory).subscribe(() => {
      this.loadCategories();
      this.alertService.successMsg('Categories deleted');
    });
  }
}
