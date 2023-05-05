using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloTravelAgent.Model.Entities
{

    public class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public List<Accommodation> Accommodations { get; set; } = new List<Accommodation>();
        public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
        public List<TouristAttraction> TouristAttractions { get; set; } = new List<TouristAttraction>();
    }
}
