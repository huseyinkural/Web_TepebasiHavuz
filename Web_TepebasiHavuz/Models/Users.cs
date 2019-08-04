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

        public string TC { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Pass { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }

        public string Degree { get; set; }

        public string DateOfBirth { get; set; }

        public string BloodGroup { get; set; }

        public string ParentInfo { get; set; }

        public string PillDetail { get; set; }

        public string IllnessDetail { get; set; }

        public string IsTrainer { get; set; }
    }
}
