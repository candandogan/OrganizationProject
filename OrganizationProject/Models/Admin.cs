using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Models
{
    public class Admin
    {
        [Display(Name = "ID")]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "Ad-Soyad alanı boş bırakılamaz.")]
        [Display(Name = "Ad-Soyad")]
        public string FullName { get; set; }

        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Parola alanı boş bırakılamaz.")]
        public string Password { get; set; }

        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "E-posta alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "E-posta formatına uygun bir adres giriniz.")]
        public string Email { get; set; }

        [Display(Name = "Telefon Numarası")]
        [Required(ErrorMessage = "Telefon numarası alanı boş bırakılamaz.")]
        [Phone(ErrorMessage = "Girilen numara telefon numarası formatına uygun değil.")]
        public string PhoneNumber { get; set; }
    }
}
