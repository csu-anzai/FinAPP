import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  users: any;

  constructor(private client: HttpClient) { }

  ngOnInit() {
    this.doWork();
  }

  async doWork() {
    this.users = await this.getJsonResult("user", "getUsers").toPromise();
  }

  async deleteUser(user: any) {
    console.log(await this.postJsonResult("user", "deleteUser", user).toPromise());
  }

  // Transfer to seperate service to use in different components

  getJsonResult(controller: string, action: string): Observable<any> {
    return this.client.get("https://localhost:44397/api/" + controller + "/" + action);
  } 

  postJsonResult(controller: string, action: string, data: any): Observable<any> {
    return this.client.post("https://localhost:44397/api/" + controller + "/" + action, data);
  }
}
