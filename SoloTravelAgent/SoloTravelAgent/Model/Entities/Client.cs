﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloTravelAgent.Model.Entities
{
    public class Client : User
    {
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
   
}
