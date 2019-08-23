import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { User } from '../models/user';
import { tap, catchError, map } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { RecoverPassword } from '../models/recoverPassword';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  baseUrl = 'https://localhost:44397/api/users';

  private handleError(error: any) {
    console.log(error);
    return throwError(error);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}`).pipe(
      tap(data => console.log(data)),
      catchError(this.handleError)
    );
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/${id}`).pipe(
      tap((receivedData: User) => {
        return receivedData;
      }),
      catchError(this.handleError)
    );
  }

  register(user: User) {
    return this.http.post(`${this.baseUrl}/register`, user);
  }

  update(user: User): Observable<User> {
    return this.http.put<User>(`${this.baseUrl}/${user.id}`, user).pipe(
      map(() => user),
      catchError(this.handleError)
    );
  }

  deleteUser(id: number) {
    const url = `${this.baseUrl}/${id}`;
    return this.http.delete(url).pipe(
      catchError(this.handleError)
    );
  }

  recoverPassword(model: RecoverPassword) {
    return this.http.put(this.baseUrl + '/recoverPassword', model);
  }
}
