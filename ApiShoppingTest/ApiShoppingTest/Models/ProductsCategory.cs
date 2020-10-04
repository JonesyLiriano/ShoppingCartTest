using System;
using System.Collections.Generic;

namespace ApiShoppingTest.Models
{
    public partial class ProductsCategory
    {
        public ProductsCategory()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
