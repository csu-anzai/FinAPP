import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { User } from '../models/user';
import { tap, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }
  
  baseUrl = 'https://localhost:44397/api/user';

  private handleError(error: any) {
    console.log(error);
    return throwError(error);
  }

  getAll() {
      return this.http.get<User[]>(`${this.baseUrl}`);
  }

  getById(id: number) : Observable<User> {
     return this.http.get<User>(`${this.baseUrl}/${id}`).pipe (
       tap((receivedData: User) => {
         console.log(receivedData);
         return receivedData; 
        }),
        catchError(this.handleError)
       );
  }

  register(user: User) {
      return this.http.post(`${this.baseUrl}/users/register`, user);
  }

  update(user: User) {
      return this.http.put(`${this.baseUrl}/${user.id}`, user);
  }

  delete(id: number) {
      return this.http.delete(`${this.baseUrl}/users/${id}`);
  }

  
}
