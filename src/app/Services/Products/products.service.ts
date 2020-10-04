import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  api = '/Products';
  constructor(private httpClient: HttpClient) { }

  GetProducts() {
    return this.httpClient.get<any>(environment.BaseUrl + this.api);
  }

  PostProduct(product) {
    return this.httpClient.post<any>(environment.BaseUrl + this.api, product);
  }
  PutProduct(product, id) {
    return this.httpClient.put<any>(environment.BaseUrl + this.api + '/' + id,
    {"id": +product.Id,
    "idProductCategory": product.idProductCategory,
    "productCode": product.productCode,
    "description": +product.description,
    "price": +product.price,
    "Available": +product.Available,
    "Orders": +product.CreditLimit,
    "rowVersion": product.Orders});
  }

  DeleteProduct(id: number) {
    return this.httpClient.delete<any>(environment.BaseUrl + this.api + '/' + id);
  }
}
