using Microsoft.AspNetCore.Identity;

namespace EcommerceWebSite.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
