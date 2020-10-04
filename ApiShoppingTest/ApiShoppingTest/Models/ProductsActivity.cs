using System;
using System.Collections.Generic;

namespace ApiShoppingTest.Models
{
    public partial class ProductsActivity
    {
        public int IdProduct { get; set; }
        public int IdCustomer { get; set; }
        public string TypeTransaction { get; set; }
        public string ActivityNumber { get; set; }
        public DateTime ActivityDate { get; set; }
        public decimal ActivityAmount { get; set; }
        public int ProductQty { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Customers IdCustomerNavigation { get; set; }
        public virtual Products IdProductNavigation { get; set; }
    }
}
