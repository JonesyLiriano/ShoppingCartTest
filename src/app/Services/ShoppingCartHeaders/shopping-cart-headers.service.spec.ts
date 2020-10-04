import { TestBed } from '@angular/core/testing';

import { ShoppingCartHeadersService } from './shopping-cart-headers.service';

describe('ShoppingCartHeadersService', () => {
  let service: ShoppingCartHeadersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShoppingCartHeadersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
