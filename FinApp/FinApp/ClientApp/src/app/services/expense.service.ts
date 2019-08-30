import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { NotificationService } from './notification.service';
import { ErrorHandlingService } from './error-handling.service';


@Injectable({
  providedIn: 'root'
})
export class ExpenseService {

  baseUrl = 'https://localhost:44397/api/expenses';

  constructor(private http: HttpClient,
    private errorHandler: ErrorHandlingService,
    private alertService: NotificationService,) { }

  addExpense(model) {
    return this.http.post(`${this.baseUrl}/add`, model).pipe(
      tap(data => this.alertService.successMsg("Expense added successful")),
      catchError(error => this.errorHandler.handleError(error))
    );
  }
}
