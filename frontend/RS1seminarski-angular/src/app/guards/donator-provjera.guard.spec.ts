import { TestBed } from '@angular/core/testing';

import { DonatorProvjeraGuard } from './donator-provjera.guard';

describe('DonatorProvjeraGuard', () => {
  let guard: DonatorProvjeraGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(DonatorProvjeraGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
