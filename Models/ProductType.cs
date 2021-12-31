using System;
using System.Collections.Generic;

#nullable disable

namespace DManage.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            Pallates = new HashSet<Pallate>();
            ProductMasters = new HashSet<ProductMaster>();
        }

        public int ProductTypeId { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Pallate> Pallates { get; set; }
        public virtual ICollection<ProductMaster> ProductMasters { get; set; }
    }
}
