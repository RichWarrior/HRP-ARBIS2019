using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRP.Arbis.Models
{
    public class SchoolViewModel
    {
        public string Id { get; set; }
        [Display(Name="Okul Adı")]
        public string Name { get; set; }
        [Display(Name="Okul E-Posta")]
        public string E_Mail { get; set; }
        public List<SchoolTypeViewModel> type { get; set; }
        public SchoolViewModel()
        {
            type = new List<SchoolTypeViewModel>();
        }

    }
    public class SchoolCreateModel
    {
        public string Id { get; set; }
        [Display(Name = "Okul Adı")]
        [MaxLength(50,ErrorMessage ="50 Karekterden Fazla Uzun Olamaz")]
        public string Name { get; set; }
        [Display(Name = "Kurum Kodu")]
        [MaxLength(50, ErrorMessage = "50 Karekterden Fazla Uzun Olamaz")]
        public string E_Mail { get; set; }
        [Display(Name = "Okul Türü")]
        [Required]
        public string type_id { get; set; }


    }
    public class SchoolProfileViewModel
    {
        public string Id { get; set; }
        [Display(Name="Okul Adı")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Email")]
        [Required]
        public string E_Mail { get; set; }
        [Display(Name="İl")]
        [Required]
        public int? CityId { get; set; }
        [Display(Name = "İlçe")]
        [Required]
        public int? District_Id { get; set; }
        [Display(Name = "Telefon Numarası")]
        [Required]
        public string phoneNumber { get; set; }
        [Display(Name = "Adres")]
        [Required]
        public string address { get; set; }
        public string type_id { get; set; }
        [Display(Name="Yeni Şifre")]
        public string Password { get; set; }

        [Display(Name="İl")]
        public List<CountryViewModel> City { get; set; }
        [Display(Name = "İlçe")]
        public List<DistrictViewModel> District { get; set; }
        public List<SchoolTypeViewModel> type { get; set; }

        public SchoolProfileViewModel()
        {
            City = new List<CountryViewModel>();
            District = new List<DistrictViewModel>();
            type = new List<SchoolTypeViewModel>();
        }
    }

    public class SchoolTypeViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}