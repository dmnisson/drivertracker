import { TestBed } from '@angular/core/testing';

import { LegService } from './leg.service';

describe('LegService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LegService = TestBed.get(LegService);
    expect(service).toBeTruthy();
  });
});
