using System;
using System.Collections.Generic;

#nullable disable

namespace DManage.Models
{
    public partial class ProductMaster
    {
        public ProductMaster()
        {
            ProductInventories = new HashSet<ProductInventory>();
        }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductTypeId { get; set; }
        public string Lot { get; set; }

        public virtual ProductType ProductType { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
    }
}
