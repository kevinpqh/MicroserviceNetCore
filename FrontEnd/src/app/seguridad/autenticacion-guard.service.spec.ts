import { TestBed } from '@angular/core/testing';

import { AutenticacionGuardService } from './autenticacion-guard.service';

describe('AutenticacionGuardService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AutenticacionGuardService = TestBed.get(AutenticacionGuardService);
    expect(service).toBeTruthy();
  });
});
