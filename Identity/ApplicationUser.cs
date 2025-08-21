using Microsoft.AspNetCore.Identity;

namespace EcommerceWebSite.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name    { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}


