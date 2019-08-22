import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Category } from '../models/category';
import { tap, catchError, map } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  baseUrl = 'https://localhost:44397/api';
  headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');
  httpOptions = {
    headers: this.headers
  };


  private handleError(error: any) {
    console.log(error);
    return throwError(error);
  }

  getCategories(isIncomeCategories: boolean): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.baseUrl}/${isIncomeCategories ? "incomecategory" : "expensecategory"}`).pipe(
      tap(data => console.log(data)),
      catchError(this.handleError)
    );
  }

  getCategory(id: number): Observable<Category> {
    return this.http.get<Category>(`${this.baseUrl}/${id}`).pipe(
      tap((receivedData: Category) => {
        return receivedData;
      }),
      catchError(this.handleError)
    );
  }

  creationExpenseCategory(category: Category, isIncomeCategories: boolean) {
    const url = `${this.baseUrl}/${"expensecategory"}/createExpenseCategory`;
    return this.http.post(url, category);
  }

  creationIncomeCategory(category: Category, isIncomeCategories: boolean) {
    const url = `${this.baseUrl}/${"incomecategory"}/createIncomeCategory`;
    return this.http.post(url, category);
  }


  updateCategory(category: Category, isIncomeCategories: boolean): Observable<Category> {
    const url = `${this.baseUrl}/${isIncomeCategories ? "incomecategory" : "expensecategory"}/`;
    return this.http.put<Category>(url + `${category.id}`, category).pipe(
      map(() => category),
      catchError(this.handleError)
    );
  }

  deleteCategory(id: number, isIncomeCategories: boolean) {
    const url = `${this.baseUrl}/${isIncomeCategories ? "incomecategory" : "expensecategory"}/${id}`;
    return this.http.delete(url, this.httpOptions).pipe(
      catchError(this.handleError)
    );
  }

}
