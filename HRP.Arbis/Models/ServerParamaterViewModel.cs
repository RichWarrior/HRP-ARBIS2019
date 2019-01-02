using System.ComponentModel.DataAnnotations;

namespace HRP.Arbis.Models
{
    public class ServerParamaterViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Anahtar")]
        [Required]
        public string key_str { get; set; }
        [Display(Name = "Değer")]
        [Required]
        public string value_str { get; set; }
    }
}