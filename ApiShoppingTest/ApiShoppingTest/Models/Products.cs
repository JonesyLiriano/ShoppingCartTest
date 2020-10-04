using System;
using System.Collections.Generic;

namespace ApiShoppingTest.Models
{
    public partial class Products
    {
        public Products()
        {
            InvoiceItems = new HashSet<InvoiceItems>();
            ProductsActivity = new HashSet<ProductsActivity>();
            ShoppingCartItems = new HashSet<ShoppingCartItems>();
        }

        public int Id { get; set; }
        public int IdProductCategory { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Available { get; set; }
        public int? Orders { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ProductsCategory IdProductCategoryNavigation { get; set; }
        public virtual ICollection<InvoiceItems> InvoiceItems { get; set; }
        public virtual ICollection<ProductsActivity> ProductsActivity { get; set; }
        public virtual ICollection<ShoppingCartItems> ShoppingCartItems { get; set; }
    }
}
