using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRP.Arbis.Models
{
    public class HandshakeViewModel
    {
        public string Id { get; set; }
        [Display(Name="Başlık")]
        public string Name { get; set; }
        [Display(Name="Adet")]
        public int price { get; set; }
        [Display(Name="Kategori")]
        public string Category { get; set; }
        [Display(Name="Okul Adı")]
        public string SchoolName { get; set; }
        [Display(Name="Telefon")]
        public string phone { get; set; }
        [Display(Name="E-Posta")]
        public string email { get; set; }

        [Display(Name="Kategori")]
        public string category_id { get; set; }

        public List<Category> cat { get; set; }
        public HandshakeViewModel()
        {
            cat = new List<Category>();
        }

    }
    public class HandshakeModel
    {
        public List<HandshakeViewModel> view { get; set; }
        public List<CountryViewModel> city { get; set; }
        public List<Category> cat { get; set; }

        public HandshakeModel()
        {
            view = new List<HandshakeViewModel>();
            city = new List<CountryViewModel>();
            cat = new List<Category>();
        }
    }
}