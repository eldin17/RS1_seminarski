import { TestBed } from '@angular/core/testing';

import { AdminProvjeraGuard } from './admin-provjera.guard';

describe('AdminProvjeraGuard', () => {
  let guard: AdminProvjeraGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AdminProvjeraGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
