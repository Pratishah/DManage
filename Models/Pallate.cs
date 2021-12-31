using System;
using System.Collections.Generic;

#nullable disable

namespace DManage.Models
{
    public partial class Pallate
    {
        public Pallate()
        {
            ProductInventories = new HashSet<ProductInventory>();
        }

        public Guid PallateId { get; set; }
        public int ProductTypeId { get; set; }
        public int Capacity { get; set; }
        public int NodeId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDatetime { get; set; }
        public DateTime? CreateDatetime { get; set; }

        public virtual Node Node { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual ICollection<ProductInventory> ProductInventories { get; set; }
    }
}
