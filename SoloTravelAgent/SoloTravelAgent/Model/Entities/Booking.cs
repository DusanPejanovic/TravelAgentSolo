using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloTravelAgent.Model.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public Trip Trip { get; set; }
        public DateTime BookingDate { get; set; }
        public bool IsPaid { get; set; }
    }
}
