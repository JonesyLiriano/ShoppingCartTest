using System;
using System.Collections.Generic;

namespace ApiShoppingTest.Models
{
    public partial class ShoppingCartHeader
    {
        public ShoppingCartHeader()
        {
            ShoppingCartItems = new HashSet<ShoppingCartItems>();
        }

        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int IdCustomer { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderAmount { get; set; }
        public bool IsCredit { get; set; }
        public bool Active { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Customers IdCustomerNavigation { get; set; }
        public virtual ICollection<ShoppingCartItems> ShoppingCartItems { get; set; }
    }
}
