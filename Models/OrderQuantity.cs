using System;
using System.Collections.Generic;

#nullable disable

namespace DManage.Models
{
    public partial class OrderQuantity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
    }
}
