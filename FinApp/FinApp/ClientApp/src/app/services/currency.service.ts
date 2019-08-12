import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Currency } from '../models/currency';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {

  url = 'https://localhost:44397/api/currency/';

  constructor(public http: HttpClient) { }

  getAll(): Observable<Currency[]> {
    return this.http.get<Currency[]>(this.url+"getAll")
      .pipe(tap((data: Currency[]) => {
        return data;
      }));
  }
}
