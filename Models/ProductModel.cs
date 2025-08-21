using System.ComponentModel;

namespace EcommerceWebSite.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [DisplayName("Ürün Adı")]
        public string Name { get; set; } = "";

        [DisplayName("Ürün Açıklaması")]
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
    }
}