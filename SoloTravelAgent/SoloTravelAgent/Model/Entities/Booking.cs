using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloTravelAgent.Model.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public virtual Client Client { get; set; }
        public virtual Trip Trip { get; set; }
        public DateTime BookingDate { get; set; }
        public bool IsPaid { get; set; }
    }
}
