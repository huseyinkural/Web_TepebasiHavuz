using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web_TepebasiHavuz.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public Users User { get; set; }
       

      
        public int PoolID { get; set; }
        [ForeignKey("PoolID")]
        public PoolDB Pool { get; set; }

    }
}
