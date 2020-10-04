using System;
using System.Collections.Generic;

namespace ApiShoppingTest.Models
{
    public partial class InvoiceHeader
    {
        public InvoiceHeader()
        {
            InvoiceItems = new HashSet<InvoiceItems>();
        }

        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int IdCustomer { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public bool Active { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Customers IdCustomerNavigation { get; set; }
        public virtual ICollection<InvoiceItems> InvoiceItems { get; set; }
    }
}
