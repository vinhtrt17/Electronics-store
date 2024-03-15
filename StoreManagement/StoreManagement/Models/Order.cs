﻿using System;
using System.Collections.Generic;

#nullable disable

namespace StoreManagement.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string Uname { get; set; }
        public DateTime Orderdate { get; set; }
        public int Total { get; set; }

        public virtual User UnameNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
