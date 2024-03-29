﻿using Microsoft.EntityFrameworkCore;
using SoloTravelAgent.Model.Entities;
using System.IO;

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
        public DbSet<TripTouristAttraction> TripTouristAttractions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var folder = Directory.GetCurrentDirectory();
            var DbPath = Path.Join(folder, "..\\..\\..\\");
            DbPath = Path.Join(DbPath, "travel_system.db");
            optionsBuilder.UseSqlite($"Data Source={DbPath}").UseLazyLoadingProxies();
        }
    }
}
