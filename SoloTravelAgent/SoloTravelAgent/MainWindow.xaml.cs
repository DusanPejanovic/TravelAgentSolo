using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoloTravelAgent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new TravelSystemDbContext())
            {
                // Apply any pending migrations to the database
                dbContext.Database.EnsureCreated();

                var tripService = new TripService(dbContext);

                // Add a new trip
                var newTrip = new Trip
                {
                    Name = "Sample Trip",
                    Description = "A wonderful journey!",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7),
                    Price = 1200.00m
                };
                tripService.AddTrip(newTrip);

                // Retrieve all trips
                var trips = tripService.GetAllTrips();
                foreach (var trip in trips)
                {
                    Debug.WriteLine($"Trip: {trip.Name}, Start date: {trip.StartDate}, Price: {trip.Price}");
                }
            }
        }
    }
}
