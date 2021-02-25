using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Models
{
    public class ConfirmThing
    {
        [Display(Name = "ID")]
        public int ConfirmThingId { get; set; }

        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "Başlık alanı boş bırakılamaz.")]
        public string Title { get; set; }

        [Display(Name = "Detaylar")]
        [Required(ErrorMessage = "Detay alanı boş bırakılamaz.")]
        public string Detail { get; set; }

        //number of confirms required
        [Display(Name = "Minimum Gerekli Katılımcı Sayısı")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public int NumOfConfReq { get; set; }

        [Display(Name = "Organizasyonun Katılıma Kapanacağı Tarih")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]

        public DateTime Deadline { get; set; }

        //Bir başlığı birden çok kullanıcı seçebilir

        public IList<ConfirmThingUser> ConfirmThingUsers { get; set; }
    }
}
