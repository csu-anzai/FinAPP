import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessagingCenterService {

  private message = new BehaviorSubject({});

  currentParams = this.message.asObservable();

  constructor() { }

  passParameters(params: any) {
    this.message.next(params);
  }
}
