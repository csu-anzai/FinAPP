import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, Observer } from 'rxjs';
import { Image } from '../models/image';
import { tap, catchError, map } from 'rxjs/operators';
import { ErrorHandlingService } from './error-handling.service';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  url = 'https://localhost:44397/api/images/';
  private handleError(error: any) {
    console.log(error);
    return throwError(error);
  }
  headers = new HttpHeaders().set('Content-Type', 'application/json').set('Accept', 'application/json');
  httpOptions = {
    headers: this.headers
  };

  constructor(public http: HttpClient,
     private errorHandler: ErrorHandlingService) { }

  getAll(): Observable<Image[]> {
    return this.http.get<Image[]>(this.url + "getAll")
      .pipe(tap(
        (data: Image[]) => {
          return data;
        },
        error => this.errorHandler.handleError(error)
      ));
  }

  deleteImage(id: number) {
    const url = `${this.url}/${id}`;
    return this.http.delete(url, this.httpOptions).pipe(
      catchError(this.handleError));
  }

  changeImage(image: Image): Observable<Image> {
    const url = `${this.url}/`;
    return this.http.put<Image>(url + `${image.id}`, image).pipe(
      map(() => image),
      catchError(this.handleError))
  }

  createImage(image: Image) {
    const url = `${this.url}/createImage`;
    return this.http.post(url, image);
  }

  getImage(id: number): Observable<Image> {
    return this.http.get<Image>(`${this.url}/${id}`).pipe(
      tap((receivedData: Image) => {
        return receivedData;
      }),
      catchError(this.handleError)
    );
  }
}
