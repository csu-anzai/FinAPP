import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { map, switchMap, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import ImageConvertor from '../helpers/imageConvertor';

@Injectable({
  providedIn: 'root'
})
export class UploadService {
  baseUrl = 'https://localhost:44397/api/';
  fileContent: string | ArrayBuffer = '';

  constructor(private http: HttpClient) { }

  uploadUserAvatar(id: number, avatar: File) {
    const fileReader: FileReader = new FileReader();
    const self = this;
    fileReader.onloadend = function (e) {
      self.fileContent = fileReader.result;
      self.http.post(self.baseUrl + 'upload/', { 'userId': id, 'avatar': self.fileContent }).toPromise();
    };
    fileReader.readAsDataURL(avatar);
  }

  uploadCategoryImage(image: File) {
    const attachedFile = ImageConvertor.fromFileToFormData(image);
    const uploadReq = new HttpRequest('POST', this.baseUrl + 'images', attachedFile, {
      reportProgress: true,
    });

    return this.http.request(uploadReq).pipe(
      catchError(err => throwError(err))
    );
  }
}
