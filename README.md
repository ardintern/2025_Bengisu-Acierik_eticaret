EcommerceWebSite
ASP.NET Core MVC + Entity Framework Core + SQLite + Identity ile hazırlanmış basit bir e-ticaret uygulaması.
Sepet (Session), sipariş oluşturma, kullanıcı girişi/kayıt, admin sipariş yönetimi ve ürün listesi içerir.
Özellikler
🛒 Sepet: Session tabanlı CART anahtarı ile saklanır (miktar artır/azalt, sil, toplam tutar).
👤 Hesap: Identity ile Login/Logout/Register.
📦 Siparişlerim: Kullanıcı adına filtrelenmiş sipariş listesi ve detayları (/Account).
🧑‍💼 Yönetim: Admin rolü için sipariş yönetimi (/Order).
🗂️ Ürünler: Listeleme, ürün detayına gitme, sepete ekleme.
🧱 Veritabanı: EF Core + SQLite, otomatik migrasyon ve seed.
Kullanılan Teknolojiler
ASP.NET Core MVC (.NET 7/8)
Entity Framework Core (SQLite)
ASP.NET Core Identity
Bootstrap 5, Font Awesome
Proje Yapısı (özet)
EcommerceWebSite/
├─ Controllers/
│  ├─ AccountController.cs         # Login/Register/Logout, /Account (Sipariş Bilgileriniz), Details
│  ├─ CartController.cs            # Sepet işlemleri (Session)
│  ├─ OrderController.cs           # [Authorize(Roles="admin")] admin sipariş yönetimi
│  └─ ProductController.cs         # Ürün yönetimi / listeleme (sizinkine göre)
├─ Entity/
│  ├─ DataContext.cs               # DbSet<Product>, DbSet<Order>, DbSet<OrderLine> ...
│  ├─ Order.cs, OrderLine.cs, EnumOrderState.cs
│  └─ (Product, Category, vs.)
├─ Identity/
│  ├─ ApplicationUser.cs, ApplicationRole.cs, IdentityDataContext.cs
│  ├─ IdentityInitializer.cs
│  └─ DataInitializer.cs
├─ Models/
│  ├─ Cart.cs (CartLine ile)       # Session’a serialize edilir
│  ├─ AdminOrderModel.cs
│  ├─ UserOrderModel.cs
│  └─ OrderDetailsModel.cs
├─ Views/
│  ├─ Shared/_Layout.cshtml        # Aktif menü vurgusu eklendi
│  ├─ Shared/_LoginPartial.cshtml  # Kullanıcı adı + sepet ikonu/rozeti
│  ├─ Account/Index.cshtml         # Sipariş Bilgileriniz
│  ├─ Account/Details.cshtml
│  ├─ Cart/Index.cshtml
│  ├─ Cart/Summary.cshtml
│  └─ Home/Index.cshtml            # Hero + Ürün listesi
└─ Program.cs                      # Otomatik Migrate + Seed + Session + Identity
