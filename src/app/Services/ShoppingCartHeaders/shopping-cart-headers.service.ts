import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartHeadersService {
  api = '/ShoppingCartHeaders';
  constructor(private httpClient: HttpClient) { }

  PostShoppingCartHeader(shoppingCartHeader) {
    return this.httpClient.post<any>(environment.BaseUrl + this.api, {
      "orderNumber": shoppingCartHeader.orderNumber.toString(),
      "idCustomer": +shoppingCartHeader.idCustomer,
      "orderDate": shoppingCartHeader.orderDate,
      "orderAmount": +shoppingCartHeader.orderAmount,
      "isCredit": !!shoppingCartHeader.isCredit,
      "active": !!shoppingCartHeader.active
  });
}
}
