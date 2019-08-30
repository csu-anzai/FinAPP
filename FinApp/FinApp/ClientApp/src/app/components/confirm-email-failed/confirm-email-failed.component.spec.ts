import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmEmailFailedComponent } from './confirm-email-failed.component';

describe('ConfirmEmailFailedComponent', () => {
  let component: ConfirmEmailFailedComponent;
  let fixture: ComponentFixture<ConfirmEmailFailedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfirmEmailFailedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmEmailFailedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
