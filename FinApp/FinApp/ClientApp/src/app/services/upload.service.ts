import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UploadService {
  baseUrl = 'https://localhost:44397/api/';
  http: HttpClient;
  public base64data: string;
  fileContent: string | ArrayBuffer = '';

  constructor(httpService: HttpClient) {
    this.http = httpService;
  }

  uploadUserAvatar(id: number, avatars: File[]) {
    let file = avatars[0];
    let fileReader: FileReader = new FileReader();
    let self = this;
    fileReader.onloadend = function(x) {
      self.fileContent = fileReader.result;
      console.log(self.fileContent);
      self.http.post(self.baseUrl + 'upload/', {'userId': id, 'avatar': self.fileContent});
      console.log('opa-opa-tishe-tishe');
    }
    fileReader.readAsText(file);
  }
}
