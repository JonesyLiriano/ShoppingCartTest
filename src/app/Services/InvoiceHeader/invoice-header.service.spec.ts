import { TestBed } from '@angular/core/testing';

import { InvoiceHeaderService } from './invoice-header.service';

describe('InvoiceHeaderService', () => {
  let service: InvoiceHeaderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InvoiceHeaderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
