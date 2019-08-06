import { NotificationService } from 'src/app/services/notification.service';
import { TestBed, inject } from '@angular/core/testing';


describe('NotificationService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NotificationService]
    });
  });

  it('should be created', inject([NotificationService], (service: NotificationService) => {
    expect(service).toBeTruthy();
  }));
});
