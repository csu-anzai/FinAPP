import { TestBed, inject } from '@angular/core/testing';

import { MessagingCenterService } from './messaging-center.service';

describe('MessagingCenterService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MessagingCenterService]
    });
  });

  it('should be created', inject([MessagingCenterService], (service: MessagingCenterService) => {
    expect(service).toBeTruthy();
  }));
});
