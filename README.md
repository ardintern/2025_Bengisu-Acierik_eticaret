# EcommerceWebSite

ASP.NET Core MVC + Entity Framework Core + SQLite + Identity ile hazırlanmış basit bir e-ticaret uygulaması.  
Sepet (Session), sipariş oluşturma, kullanıcı girişi/kayıt, admin sipariş yönetimi ve ürün listesi içerir.

## Özellikler
- 🛒 **Sepet**: Session tabanlı `CART` anahtarı ile saklanır (miktar artır/azalt, sil, toplam tutar).
- 👤 **Hesap**: Identity ile Login/Logout/Register.
- 📦 **Siparişlerim**: Kullanıcı adına filtrelenmiş sipariş listesi ve detayları (`/Account`).
- 🧑‍💼 **Yönetim**: Admin rolü için sipariş yönetimi (`/Order`).
- 🗂️ **Ürünler**: Listeleme, detay, sepete ekleme.
- 🧱 **Veritabanı**: EF Core + SQLite, otomatik migrasyon ve seed.



