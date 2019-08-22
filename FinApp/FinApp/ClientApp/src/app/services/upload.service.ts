import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UploadService {
  baseUrl = 'https://localhost:44397/api/';
  fileContent: string | ArrayBuffer = '';

  constructor(private http: HttpClient) {}

  uploadUserAvatar(id: number, avatars: File[]) {
    const file = avatars[0];
    const fileReader: FileReader = new FileReader();
    const self = this;
    fileReader.onloadend = function(e) {
      self.fileContent = fileReader.result;
      console.log(self.fileContent);
      self.http.post(self.baseUrl + 'upload/', {'userId': id, 'avatar': self.fileContent})
      .toPromise()
      .then(
        (response: any) => {
          console.log(response);
        }
      );
    };
    fileReader.readAsDataURL(file);
  }
}
