using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationProject.Models
{
    public class ConfirmThingUser
    {
       

        [Display(Name = "Kullanıcı")]
        public int UserId { get; set; }
        [Display(Name = "Kullanıcı")]
        public User User { get; set; }

        [Display(Name = "Organizasyon")]

        public int ConfirmThingId { get; set; }
        [Display(Name = "Organizasyon")]
        public ConfirmThing ConfirmThing { get; set; }

        [Display(Name = "Katılım Durumu")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz.")]
        public bool Vote { get; set; }

        [Display(Name = "Mesaj")]
        public string Note { get; set; }
    }
}
