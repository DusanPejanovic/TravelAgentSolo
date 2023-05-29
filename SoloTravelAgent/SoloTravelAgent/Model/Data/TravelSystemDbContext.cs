using Microsoft.EntityFrameworkCore;
using SoloTravelAgent.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloTravelAgent.Model.Data
{
    public class TravelSystemDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<TouristAttraction> TouristAttractions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\Anastasija\\Desktop\\HCI-TravelAgent\\TravelAgentSolo\\SoloTravelAgent\\SoloTravelAgent\\travel_system.db").UseLazyLoadingProxies(); ;
        }
    }
}
