import { TestBed, inject } from '@angular/core/testing';

import { RegisterValidateService } from './register-validate.service';

describe('RegisterValidateService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RegisterValidateService]
    });
  });

  it('should be created', inject([RegisterValidateService], (service: RegisterValidateService) => {
    expect(service).toBeTruthy();
  }));
});
