using System;
using System.Collections.Generic;

namespace ApiShoppingTest.Models
{
    public partial class InvoiceItems
    {
        public int Id { get; set; }
        public int IdInvoice { get; set; }
        public int Row { get; set; }
        public int IdProduct { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string OrderNumber { get; set; }
        public int? OrderRow { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual InvoiceHeader IdInvoiceNavigation { get; set; }
        public virtual Products IdProductNavigation { get; set; }
    }
}
