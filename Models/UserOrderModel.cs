using System;
using EcommerceWebSite.Entity;  // EnumOrderState

namespace EcommerceWebSite.Models
{
    public class UserOrderModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = default!;
        public double Total { get; set; }
        public EnumOrderState OrderState { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
