using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_TepebasiHavuz.Models
{
    public class OnKayit
    {
        [Key]
        public int UserID { get; set; }

        [RegularExpression(@"^[1-9]{1}[0-9]{10}$",
            ErrorMessage = "Lütfen geçerli bir TC Kimlik Numarası girin")]
        [Required(ErrorMessage = "TC boş bırakılamaz.")]
        public string TC { get; set; }

        [Required(ErrorMessage = "İsim Soyisim boş bırakılamaz.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Cinsiyet boş bırakılamaz.")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Derece boş bırakılamaz.")]
        public string Degree { get; set; }

        [Required(ErrorMessage = "Adres boş bırakılamaz.")]
        public string User_Address { get; set; }

        [Required(ErrorMessage = "Doğum tarihi boş bırakılamaz.")]
        public DateTime DateOfBirth { get; set; }

        
        public string BloodGroup { get; set; }

        
        public string ParentInfo { get; set; }

       
        public string PillDetail { get; set; }

      
        public string IllnessDetail { get; set; }
    }
}
