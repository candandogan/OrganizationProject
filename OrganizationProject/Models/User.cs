using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Models
{
    public class User
    {
        [Display(Name = "ID")]
        public int UserId { get; set; }

        [Display(Name = "Ad-Soyad")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string FullName { get; set; }

        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public string Password { get; set; }

        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "E-posta formatına uygun bir adres giriniz.")]
        public string Email { get; set; }

        [Display(Name = "Telefon Numarası")]
        [Required(ErrorMessage = "Telefon numarası alanı boş bırakılamaz.")]
        [Phone(ErrorMessage = "Girilen numara telefon numarası formatına uygun değil.")]
        public string PhoneNumber { get; set; }

        //Bir kullanıcı birden çok başlık için oy verebilir

        public IList<ConfirmThingUser> ConfirmThingUsers { get; set; }
    }
}
