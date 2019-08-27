import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { map, switchMap, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { Image } from '../models/image';
import { bypassSanitizationTrustResourceUrl } from '@angular/core/src/sanitization/bypass';

@Injectable({
  providedIn: 'root'
})
export class UploadService {
  baseUrl = 'https://localhost:44397/api/';
  fileContent: string | ArrayBuffer;

  constructor(private http: HttpClient) { }

  uploadUserAvatar(id: number, avatar: File) {
    const attachedInfo  = new FormData();
    attachedInfo.append('avatar', avatar);
    attachedInfo.append('userId', id.toString());

    const uploadReq = new HttpRequest('POST', `${this.baseUrl}upload/`, attachedInfo, {
      reportProgress: true,
    });

    return this.http.request(uploadReq).pipe(
      catchError(err => throwError(err))
    );
  }

  uploadCategoryImage(imageInfo: Image, imageFile: File) {
    // setting up a request
    const attachedInfo  = new FormData();
    attachedInfo.append('image', imageFile);
    attachedInfo.append('name', imageInfo.name);
    attachedInfo.append('path', imageInfo.path);

    const uploadReq = new HttpRequest('POST', `${this.baseUrl}images/`, attachedInfo, {
      reportProgress: true,
    });

    return this.http.request(uploadReq).pipe(
      catchError(err => throwError(err))
    );
  }
}
