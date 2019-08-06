import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { User } from '../models/user';
import { tap, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }
  
  baseUrl = 'https://localhost:44397/api/user';

  getAll() {
      return this.http.get<User[]>(`${this.baseUrl}`);
  }

  getById(id: number) : Observable<User> {
     const url = `${this.baseUrl}/${id}`;
     //return this.http.get<User>(url);
     return this.http.get<User>(url).pipe(
       tap((receivedData: User) => {console.log(receivedData);return receivedData}));
  }

  register(user: User) {
      return this.http.post(`${this.baseUrl}/users/register`, user);
  }

  update(user: User) {
      return this.http.put(`${this.baseUrl}/users/${user.id}`, user);
  }

  delete(id: number) {
      return this.http.delete(`${this.baseUrl}/users/${id}`);
  }
  // private handleError<T> (operation = 'operation', result?: T) {
  //   return (error: any): Observable<T> => {

  //     // TODO: send the error to remote logging infrastructure
  //     console.error(error); // log to console instead

  //     // TODO: better job of transforming error for user consumption
  //     this.log(`${operation} failed: ${error.message}`);

  //     // Let the app keep running by returning an empty result.
  //     return of(result as T);
  //   };
  // }

  /** Log a HeroService message with the MessageService */
  private log(message: string) {
    
  }
  
}
