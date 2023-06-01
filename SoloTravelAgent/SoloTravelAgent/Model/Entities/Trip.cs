using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloTravelAgent.Model.Entities
{

    public class Trip
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public virtual List<Accommodation> Accommodations { get; set; } = new List<Accommodation>();
        public virtual List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
        public virtual List<TripTouristAttraction> TripTouristAttractions { get; set; } = new List<TripTouristAttraction>();
    }
}
