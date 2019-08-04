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

        public string PoolName { get; set; }

        public string Degree { get; set; }

        public string TimePeriod { get; set; }

        public string BookingStatus { get; set; }

        public int AgeInfo { get; set; }

        public string DayInfo { get; set; }

        public int KulvarNo { get; set; }
    }
}
