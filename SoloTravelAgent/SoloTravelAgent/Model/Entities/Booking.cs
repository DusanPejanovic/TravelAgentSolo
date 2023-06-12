using System;
using System.ComponentModel.DataAnnotations;

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
