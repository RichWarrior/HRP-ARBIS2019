using System.ComponentModel.DataAnnotations;

namespace HRP.Arbis.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Kurum Kodu")]
        [Required]
        public string email { get; set; }
        [Display(Name = "Şifre")]
        [Required]
        public string password { get; set; }
    }
    public class ForgotViewModel
    {
        [Display(Name = "Kurum Kodu")]
        [Required]
        public string email { get; set; }
    }
}