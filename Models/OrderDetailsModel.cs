using System;
using System.Collections.Generic;
using EcommerceWebSite.Entity; // EnumOrderState

namespace EcommerceWebSite.Models
{
    public class OrderDetailsModel
    {
        public int OrderId { get; set; }
        public string UserName { get; set; } = default!;
        public string OrderNumber { get; set; } = default!;
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; }

        public string AdresBasligi { get; set; } = default!;
        public string Adres { get; set; } = default!;
        public string Sehir { get; set; } = default!;
        public string Semt { get; set; } = default!;
        public string Mahalle { get; set; } = default!;
        public string PostaKodu { get; set; } = default!;

        public List<OrderLineModel> Orderlines { get; set; } = new();
    }

    public class OrderLineModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string Image { get; set; } = default!;
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
