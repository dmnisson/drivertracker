import { TestBed } from '@angular/core/testing';

import { PickupPredictorService } from './pickup-predictor.service';

describe('PickupPredictorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PickupPredictorService = TestBed.get(PickupPredictorService);
    expect(service).toBeTruthy();
  });
});
