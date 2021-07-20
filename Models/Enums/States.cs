using System.ComponentModel.DataAnnotations;

namespace ZarinpalGateway.Models.Enums
{
    public enum States
    {
        [Display(Name = "بدون پاسخ")]
        Redirected,

        [Display(Name = "پرداخت موفق")]
        Succeed,

        [Display(Name = "پرداخت لغو شده")]
        Failed
    }
}
