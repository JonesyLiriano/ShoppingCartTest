import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartItemsService {
  api = '/ShoppingCartItems';
  constructor(private httpClient: HttpClient) { }

  GetShoppingCartItems(){
    return this.httpClient.get<any>(environment.BaseUrl + this.api);
  }

  PostShoppingCartItem(shoppingCartItem) {
    return this.httpClient.post<any>(environment.BaseUrl + this.api, {
      "idOrder": +shoppingCartItem.idOrder,
      "row": shoppingCartItem.Row,
      "idProduct": +shoppingCartItem.idProduct,
      "description": shoppingCartItem.description,
      "qty": +shoppingCartItem.qty,
      "price": +shoppingCartItem.price,
      "total": +shoppingCartItem.total
  });
}
}
