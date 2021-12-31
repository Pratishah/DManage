using System;
using System.Collections.Generic;

#nullable disable

namespace DManage.Models
{
    public partial class ProductInventory
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Guid PallateId { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? ModifiedDatetime { get; set; }

        public virtual Pallate Pallate { get; set; }
        public virtual ProductMaster Product { get; set; }
    }
}
