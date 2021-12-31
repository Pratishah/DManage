using System;
using System.Collections.Generic;

#nullable disable

namespace DManage.Models
{
    public partial class CustomerTable
    {
        public CustomerTable()
        {
            Orders = new HashSet<Order>();
        }

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
