
export class InvoiceItem {
  id?: number;
  idInvoice: number;
  row: number;
  idProduct: number;
  description: string;
  qty: number;
  price: number;
  total: number;
  orderNumber: number;
  orderRow: number;
  rowVersion: string;
}
