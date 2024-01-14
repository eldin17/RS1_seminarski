import { TestBed } from '@angular/core/testing';

import { LoginProvjeraGuard } from './login-provjera.guard';

describe('LoginProvjeraGuard', () => {
  let guard: LoginProvjeraGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(LoginProvjeraGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
