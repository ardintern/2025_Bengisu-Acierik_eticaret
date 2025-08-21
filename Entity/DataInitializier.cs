using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
 

namespace EcommerceWebSite.Entity
{
    public static class DataInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            // Şemayı uygula (Migration'lar)
            await context.Database.MigrateAsync();

            // Kategoriler yoksa ekle
            if (!await context.Categories.AnyAsync())
            {
                var kategoriler = new List<Category>()
                {
                    new Category { Name = "Fotoğraf Makineleri", Description = "Profesyonel ve amatör kullanıcılar için DSLR ve kompakt makineler sunulur." },
                    new Category { Name = "Laptoplar",           Description = "İş, oyun ve günlük kullanım için yüksek performanslı dizüstüler." },
                    new Category { Name = "Televizyonlar",       Description = "4K, OLED ve Smart özellikli modern televizyon modelleri." },
                    new Category { Name = "Telefonlar",          Description = "En yeni akıllı telefon modelleri ve aksesuarları burada." },
                    new Category { Name = "Aksesuarlar",         Description = "Telefon, kamera ve bilgisayar aksesuarları geniş yelpazede." }
                };

                context.Categories.AddRange(kategoriler);
                await context.SaveChangesAsync();
            }

            // Ürünler yoksa ekle
            if (!await context.Products.AnyAsync())
            {
                var urunler = new List<Product>()
                {
                    new Product {
                        Name = "Canon Eos 1200D 18-55 mm DC Profesyonel Dijital Fotoğraf Makinesi",
                        Description = "24.2MP APS-C sensörü, Full HD video kaydı ve ergonomik tutuşuyla uzun süreli çekimlerde konfor sağlar.",
                        Price = 3500, Stock = 10, CategoryId = 1,
                        IsApproved = true,  IsHome = true,  Image = "1.jpg"
                    },
                    new Product {
                        Name = "Canon Eos 100D 18-55 mm DC Profesyonel Fotoğraf Makinesi",
                        Description = "Dünyanın en küçük DSLR’si; kompakt gövde ve yüksek çözünürlüklü vizör ile kullanım kolaylığı sunar.",
                        Price = 4200, Stock = 8, CategoryId = 1,
                        IsApproved = true,  IsHome = true,  Image = "2.jpg"
                    },
                    new Product {
                        Name = "Canon Eos 700D 18-55 DC DSLR Fotoğraf Makinesi",
                        Description = "Dokunmatik ekranlı, 9 noktalı AF sistemi ve hızlı seri çekim özellikleriyle hem fotoğraf hem videoda profesyonel sonuçlar verir.",
                        Price = 5200, Stock = 12, CategoryId = 1,
                        IsApproved = true,  IsHome = true,  Image = "3.jpg"
                    },
                    new Product {
                        Name = "Canon Eos 100D 18-55 mm IS STM Kit DSLR Fotoğraf Makinesi",
                        Description = "STM lens ile sessiz ve pürüzsüz odaklama sağlar; kompakt ve hafif gövdesi taşımayı kolaylaştırır.",
                        Price = 4800, Stock = 9, CategoryId = 1,
                        IsApproved = false, IsHome = true,  Image = "4.jpg"
                    },
                    new Product {
                        Name = "Canon Eos 700D + 18-55 IS STM + Çanta + 16 Gb Hafıza Kartı",
                        Description = "Başlangıç seti; çanta, hafıza kartı ve IS STM lens ile komple bir fotoğrafçılık deneyimi sunar.",
                        Price = 5600, Stock = 7, CategoryId = 1,
                        IsApproved = true,               Image = "5.jpg"
                    },

                    new Product {
                        Name = "Dell Inspiron 5567 Intel Core i5 7200U 8GB 1TB R7 M445 Windows 10",
                        Description = "17.3” geniş ekran, güçlü AMD R7 grafik kartı ve 1 TB depolama alanıyla hem iş hem de eğlence için ideal.",
                        Price = 15000, Stock = 5, CategoryId = 2,
                        IsApproved = true,  IsHome = true,  Image = "1.jpg"
                    },
                    new Product {
                        Name = "Lenovo Ideapad 310 Intel Core i7 7500U 12GB 1TB GT920M Windows 10",
                        Description = "Üst seviye i7 işlemci, 12 GB RAM ve 1 TB HDD ile çoklu görevlerde akıcı performans sağlar.",
                        Price = 16500, Stock = 4, CategoryId = 2,
                        IsApproved = true,  IsHome = true,  Image = "2.jpg"
                    },
                    new Product {
                        Name = "Asus UX310UQ-FB418T Intel Core i7 7500U 8GB 512GB SSD GT940MX Windows 10",
                        Description = "512 GB SSD sayesinde süper hızlı açılış ve uygulama yükleme süreleri sunan hafif ultrabook.",
                        Price = 18500, Stock = 6, CategoryId = 2,
                        IsApproved = false,              Image = "3.jpg"
                    },
                    new Product {
                        Name = "Asus UX310UQ-FB418T Intel Core i7 7500U 16GB 512GB SSD GT940MX Windows 10",
                        Description = "16 GB RAM ile aynı anda birden çok büyük uygulamayı akıcı şekilde çalıştırabilirsiniz.",
                        Price = 19500, Stock = 3, CategoryId = 2,
                        IsApproved = true,               Image = "4.jpg"
                    },
                    new Product {
                        Name = "Asus N580VD-DM160T Intel Core i7 7700HQ 16GB 1TB + 128GB SSD Windows 10",
                        Description = "Hibrit depolama yapısı ve güçlü GPU’su ile hem oyun hem profesyonel uygulamalarda fark yaratır.",
                        Price = 21000, Stock = 4, CategoryId = 2,
                        IsApproved = true,               Image = "1.jpg"
                    },

                    new Product {
                        Name = "Samsung 55KU7500 55” 4K Uydu Alıcılı Smart TV",
                        Description = "Kavisli ekranı, UHD çözünürlük ve Tizen OS ile sınırsız içerik ve konforlu izleme deneyimi sağlar.",
                        Price = 25000, Stock = 5, CategoryId = 3,
                        IsApproved = true,  IsHome = true,  Image = "2.jpg"
                    },
                    new Product {
                        Name = "LG 55UH615V 55” 4K Smart Wi-Fi [webOS 3.0]",
                        Description = "webOS 3.0 ile pratik akıllı TV kontrolü, net renkler ve yüksek kontrastlı görüntü sunar.",
                        Price = 23000, Stock = 6, CategoryId = 3,
                        IsApproved = false, IsHome = true,  Image = "3.jpg"
                    },
                    new Product {
                        Name = "Sony KD-55XD7005B 55” 4K Smart LED TV",
                        Description = "Triluminos ekran ve X1 işlemci ile canlı renkler, derin kontrast ve keskin detaylar elde etmenize yardımcı olur.",
                        Price = 27000, Stock = 4, CategoryId = 3,
                        IsApproved = true,               Image = "4.jpg"
                    },

                    new Product {
                        Name = "Apple iPhone 6 32 GB (Türkiye Garantili)",
                        Description = "Retina HD ekran, A8 çip ve iOS desteğiyle günlük kullanımda akıcı performans ve geniş uygulama desteği sunar.",
                        Price = 12000, Stock = 10, CategoryId = 4,
                        IsApproved = true,  IsHome = true,  Image = "5.jpg"
                    },
                    new Product {
                        Name = "Apple iPhone 7 32 GB (Türkiye Garantili)",
                        Description = "Suya dayanıklı gövde, A10 Fusion çip ve 12 MP kamera ile hızlı ve kaliteli fotoğraf-video çekimleri yaparsınız.",
                        Price = 14000, Stock = 8, CategoryId = 4,
                        IsApproved = false, IsHome = true,  Image = "1.jpg"
                    },
                    new Product {
                        Name = "Apple iPhone 6S 32 GB (Türkiye Garantili)",
                        Description = "3D Touch desteği ve daha hızlı A9 işlemci ile dokunmatik deneyimini bir üst seviyeye taşır.",
                        Price = 13500, Stock = 7, CategoryId = 4,
                        IsApproved = true,               Image = "2.jpg"
                    },
                    new Product {
                        Name = "Case 4U Manyetik Araç İçi Telefon Tutucu",
                        Description = "Güçlü mıknatıslı tasarımı ve 360° dönebilen yapısıyla cep telefonu kullanımını pratik hâle getirir.",
                        Price = 350,   Stock = 20, CategoryId = 4,
                        IsApproved = true,               Image = "3.jpg"
                    },
                    new Product {
                        Name = "Xiaomi Mi 5S 64GB (İthalatçı Garantili)",
                        Description = "Metal gövde, hızlı parmak izi sensörü ve uzun pil ömrüyle bütçe dostu üst seviye akıllı telefon deneyimi sunar.",
                        Price = 9000,  Stock = 6,  CategoryId = 4,
                        IsApproved = true,               Image = "4.jpg"
                    }
                };

                context.Products.AddRange(urunler);
                await context.SaveChangesAsync();
            }
        }
    }
}

