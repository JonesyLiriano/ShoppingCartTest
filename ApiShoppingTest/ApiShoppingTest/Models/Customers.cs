using System;
using System.Collections.Generic;

namespace ApiShoppingTest.Models
{
    public partial class Customers
    {
        public Customers()
        {
            InvoiceHeader = new HashSet<InvoiceHeader>();
            ProductsActivity = new HashSet<ProductsActivity>();
            ShoppingCartHeader = new HashSet<ShoppingCartHeader>();
        }

        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public string FullName { get; set; }
        public decimal CreditLimit { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<InvoiceHeader> InvoiceHeader { get; set; }
        public virtual ICollection<ProductsActivity> ProductsActivity { get; set; }
        public virtual ICollection<ShoppingCartHeader> ShoppingCartHeader { get; set; }
    }
}
