import { TestBed, inject } from '@angular/core/testing';
import { ChartsService } from './charts.service';

describe('AccountService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ChartsService]
    });
  });

  it('should be created', inject([ChartsService], (service: ChartsService) => {
    expect(service).toBeTruthy();
  }));
});
