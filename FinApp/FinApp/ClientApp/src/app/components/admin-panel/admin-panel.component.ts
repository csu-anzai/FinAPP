import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/category';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { NotificationService } from '../../services/notification.service';
import { async } from '@angular/core/testing';
import { P } from '@angular/core/src/render3';


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

  constructor(private categoryService: CategoryService, public userService: UserService, fb: FormBuilder, private alertService: NotificationService) {

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
    this.categoryService.creationIncomeCategory(this.category, true).subscribe(() => {
      console.log(this.category);
      this.loadCategories();
      this.alertService.successMsg('Category added');
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
