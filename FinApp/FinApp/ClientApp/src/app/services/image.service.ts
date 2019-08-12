import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Image } from '../models/image';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  url = 'https://localhost:44397/api/image/';

  constructor(public http: HttpClient) { }

  getAll(): Observable<Image[]> {
    return this.http.get<Image[]>(this.url + "getAll")
      .pipe(tap((data: Image[]) => {
        return data;
      }));
  }
}
