using System.ComponentModel.DataAnnotations;

namespace EcommerceWebSite.Entity
{
    public enum EnumOrderState
    {
        [Display(Name = "Onay Bekleniyor")]
        Waiting = 0,

        [Display(Name = "Tamamlandı")]
        Completed = 1
    }
}
