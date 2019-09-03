import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Image } from '../models/image';

@Injectable({
  providedIn: 'root'
})
export class UploadService {
  baseUrl = 'https://localhost:44397/api/';
  maxSize = 2500000;

  constructor(private http: HttpClient) { }

  uploadUserAvatar(id: number, avatar: File) {
    if (!this.isAcceptableImageSize(avatar)) {
       return throwError('Select an image with less size');
    }
    const attachedInfo = new FormData();
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
    if (this.isAcceptableImageSize(imageFile)) {
      throwError('Select an image with less size');
    }
    // setting up a request
    const attachedInfo = new FormData();
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

  updateImage(imageInfo: Image) {
    return this.http.put(`${this.baseUrl}/images/${imageInfo.id}`, imageInfo).pipe(
      map(
        (image) => console.log(image)
      )
    );
  }

  deleteImage(imageId: number) {
    return this.http.delete(`${this.baseUrl}/images/${imageId}`).pipe(
      catchError(err => {throw new Error(err); })
    );
  }

  isAcceptableImageSize(imageFile: File) {
    if (imageFile.size > this.maxSize) {
      return false;
    }
    return true;
  }
}
