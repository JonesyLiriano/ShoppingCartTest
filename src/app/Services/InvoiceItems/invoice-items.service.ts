import { InvoiceItem } from './../../Models/InvoiceItem';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InvoiceItemsService {

  api = '/InvoiceItems';
  constructor(private httpClient: HttpClient) { }

  PostInvoiceItem(invoiceItem) {
    return this.httpClient.post<any>(environment.BaseUrl + this.api, {
      "idInvoice": +invoiceItem.idInvoice,
      "row": invoiceItem.Row,
      "idProduct": +invoiceItem.idProduct,
      "description": invoiceItem.description,
      "qty": +invoiceItem.qty,
      "price": +invoiceItem.price,
      "total": +invoiceItem.total,
      "orderNumber": invoiceItem.orderNumber.toString(),
      "orderRow": +invoiceItem.orderRow
    });
  }
}
