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
        [Display(Name="Açıklama")]
        public string Description { get; set; }
        [Display(Name="Okul Adı")]
        public string SchoolName { get; set; }
        [Display(Name="iletisim")]
        public string phone { get; set; }
        public string email { get; set; }

    }
    public class HandshakeModel
    {
        public List<HandshakeViewModel> view { get; set; }
        public List<CountryViewModel> city { get; set; }

        public HandshakeModel()
        {
            view = new List<HandshakeViewModel>();
            city = new List<CountryViewModel>();
        }
    }
}