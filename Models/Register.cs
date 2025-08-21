using System.ComponentModel.DataAnnotations;

namespace EcommerceWebSite.Models
{
    public class Register
    {
        [Required, Display(Name = "Adınız")]
        public string Name { get; set; } = string.Empty;

        [Required, Display(Name = "Soyadınız")]
        public string Surname { get; set; } = string.Empty;

        [Required, Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress, Display(Name = "Eposta")]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Display(Name = "Şifre")]
        public string Password { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Display(Name = "Şifre Tekrar"),
         Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string RePassword { get; set; } = string.Empty;
    }
}
