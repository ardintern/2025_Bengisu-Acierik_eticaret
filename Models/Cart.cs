using System.Collections.Generic;
using System.Linq;
using EcommerceWebSite.Entity;

namespace EcommerceWebSite.Models
{
    public class Cart
    {
        // JSON'dan geri okunabilmesi için set gerekli
        public List<CartLine> CartLines { get; set; } = new();

        // Toplam adet (miktar)
        public int TotalQuantity => CartLines.Sum(i => i.Quantity);

        // Ürün ekle
        public void AddProduct(Product product, int quantity = 1)
        {
            if (product == null) return;
            if (quantity <= 0) quantity = 1;

            var line = CartLines.FirstOrDefault(i => i.ProductId == product.Id);
            if (line == null)
            {
                CartLines.Add(new CartLine
                {
                    ProductId = product.Id,
                    Product   = product,
                    Quantity  = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
                if (line.Product == null) line.Product = product;
            }
        }

        // Satırı tamamen sil (id ile)
        public void DeleteProduct(int productId)
            => CartLines.RemoveAll(i => i.ProductId == productId);

        // Eski imza için overload
        public void DeleteProduct(Product product)
        {
            if (product != null) DeleteProduct(product.Id);
        }

        // Alternatif isim (uyumluluk)
        public void RemoveProduct(int productId) => DeleteProduct(productId);

        // Miktar azalt (0 veya altı -> satırı kaldır)
        public void Decrease(int productId, int by = 1)
        {
            if (by <= 0) by = 1;

            var line = CartLines.FirstOrDefault(i => i.ProductId == productId);
            if (line == null) return;

            line.Quantity -= by;
            if (line.Quantity <= 0)
                CartLines.Remove(line);
        }

        // Toplam tutar
        public decimal Total()
            => CartLines.Sum(i => i.Product.Price * i.Quantity);

        // Sepeti temizle
        public void Clear() => CartLines.Clear();
    }

    public class CartLine
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public int Quantity { get; set; }
    }
}

