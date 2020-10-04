import { Customer } from 'src/app/Models/Customer';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomersService {
  api = '/Customers';
  constructor(private httpClient: HttpClient) { }

  GetCustomers() {
    return this.httpClient.get<any>(environment.BaseUrl + this.api);
  }

  PostCustomer(customer) {
    return this.httpClient.post<any>(environment.BaseUrl + this.api, {
      "documentNumber": customer.DocumentNumber,
      "fullName": customer.FullName,
      "creditLimit": +customer.CreditLimit
  });
  }
  PutCustomer(customer, id: number) {
    return this.httpClient.put<any>(environment.BaseUrl + this.api + '/' + id,
    {"id": +customer.Id,
    "documentNumber": customer.DocumentNumber,
    "fullName": customer.FullName,
    "creditLimit": +customer.CreditLimit,
    "rowVersion": customer.RowVersion});
  }

  DeleteCustomer(id: number) {
    console.log(id);
    return this.httpClient.delete<any>(environment.BaseUrl + this.api + '/' + id);
  }
}
