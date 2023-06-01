using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloTravelAgent.Model.Entities
{
    public class TripTouristAttraction
    {
        [Key]
        public int Id { get; set; }

        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }

        public int TouristAttractionId { get; set; }
        public virtual TouristAttraction TouristAttraction { get; set; }
    }

}
