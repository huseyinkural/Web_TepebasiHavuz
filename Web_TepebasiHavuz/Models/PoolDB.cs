using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_TepebasiHavuz.Models
{
    public class PoolDB
    {
        [Key]
        public int PoolID { get; set; }

        [Required(ErrorMessage = "Havuz adını giriniz.")]
        public string PoolName { get; set; }

        [Required(ErrorMessage = "Derece bilgisini giriniz.")]
        public string Degree { get; set; }

        [Required(ErrorMessage = "Saat bilgisini giriniz.")]
        public string TimePeriod { get; set; }

        [Required(ErrorMessage = "Kontenjan bilgisini giriniz.")]
        public int Limit { get; set; }

        [Required(ErrorMessage = "Kulvar bilgisini giriniz.")]
        public int KulvarNo { get; set; }

        public string BookingStatus { get; set; }

        public int AgeInfo { get; set; }

        public string DayInfo { get; set; }

     
    }
}
