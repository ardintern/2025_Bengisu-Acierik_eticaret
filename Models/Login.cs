using System.ComponentModel.DataAnnotations;

namespace EcommerceWebSite.Models
{
    public class Login
    {
        [Required, Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Display(Name = "Şifre")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
