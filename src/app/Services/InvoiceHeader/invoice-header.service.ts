import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InvoiceHeaderService {
  api = '/InvoiceHeaders';
  constructor(private httpClient: HttpClient) { }

  PostInvoiceHeader(invoiceHeader) {
    return this.httpClient.post<any>(environment.BaseUrl + this.api, {
      "invoiceNumber": invoiceHeader.invoiceNumber.toString(),
      "idCustomer": +invoiceHeader.idCustomer,
      "invoiceDate": invoiceHeader.invoiceDate,
      "invoiceAmount": +invoiceHeader.invoiceAmount,
      "active": !!invoiceHeader.active
    });
  }

}
