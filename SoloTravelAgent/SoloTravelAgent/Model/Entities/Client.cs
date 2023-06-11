using System.Collections.Generic;

namespace SoloTravelAgent.Model.Entities
{
    public class Client : User
    {
        public virtual List<Booking> Bookings { get; set; } = new List<Booking>();
    }
   
}
