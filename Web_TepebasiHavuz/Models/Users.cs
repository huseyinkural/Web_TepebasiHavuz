using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_TepebasiHavuz.Models
{
    public class Users
    {
        
        [Key]
        public int UserID { get; set; }

        [RegularExpression(@"^[1-9]{1}[0-9]{10}$",
            ErrorMessage = "Lütfen geçerli bir TC Kimlik Numarası girin")]
        [Required(ErrorMessage = "TC boş bırakılamaz.")]
        public string TC { get; set; }

        [Required]
        public string FullName { get; set; }
        
        public string Sex { get; set; }

        public string Degree { get; set; }

        public string User_Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string BloodGroup { get; set; }

        public string ParentInfo { get; set; }

        public string PillDetail { get; set; }

        public string IllnessDetail { get; set; }



    }
}
