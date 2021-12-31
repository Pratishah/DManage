using System;
using System.Collections.Generic;

#nullable disable

namespace DManage.Models
{
    public partial class Order
    {
        public Guid OrderId { get; set; }
        public string OrderStatus { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderType { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public  CustomerTable Customer { get; set; }
        public  OrderQuantity OrderNavigation { get; set; }
    }
}
