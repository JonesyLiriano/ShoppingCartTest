import { TestBed } from '@angular/core/testing';

import { ShoppingCartItemsService } from './shopping-cart-items.service';

describe('ShoppingCartItemsService', () => {
  let service: ShoppingCartItemsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShoppingCartItemsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
