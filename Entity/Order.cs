using System;
using System.Collections.Generic;

namespace EcommerceWebSite.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = default!;
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; }   // Enum’u ayrı dosyada tanımlı bırak

        public string Username { get; set; } = default!;
        public string AdresBasligi { get; set; } = default!;
        public string Adres { get; set; } = default!;
        public string Sehir { get; set; } = default!;
        public string Semt { get; set; } = default!;
        public string Mahalle { get; set; } = default!;
        public string PostaKodu { get; set; } = default!;

        public virtual List<OrderLine> Orderlines { get; set; } = new();  // null olmaması için init
    }

    public class OrderLine
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = default!;

        public int Quantity { get; set; }
        public double Price { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = default!;
    }
}
