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
using System.Windows.Media.Animation;
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

            using var dbContext = new TravelSystemDbContext();

            // Client service test
            var clientService = new ClientService(dbContext);

            var client = new Client { Name = "John Doe", Email = "john.doe@example.com", PhoneNumber = "555-1234" };
            clientService.AddClient(client);
            Debug.WriteLine("Client added: " + client.Name);

            var foundClient = clientService.GetClient(client.Id);
            Debug.WriteLine("Client found: " + foundClient.Name);

            // Agent service test
            var agentService = new AgentService(dbContext);

            var agent = new Agent { Name = "Jane Smith", Email = "jane.smith@example.com", PhoneNumber = "555-5678" };
            agentService.AddAgent(agent);
            Debug.WriteLine("Agent added: " + agent.Name);

            var foundAgent = agentService.GetAgent(agent.Id);
            Debug.WriteLine("Agent found: " + foundAgent.Name);

            // Trip service test
            var tripService = new TripService(dbContext);

            var trip = new Trip { Name = "Paris Trip", Description = "A week in Paris", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Price = 1000m };
            tripService.AddTrip(trip);
            Debug.WriteLine("Trip added: " + trip.Name);

            var foundTrip = tripService.GetTrip(trip.Id);
            Debug.WriteLine("Trip found: " + foundTrip.Name);

            // Accommodation service test
            var accommodationService = new AccommodationService(dbContext);

            var accommodation = new Accommodation { Name = "Paris Hotel", Address = "123 Paris Street", Stars = 4, Website = "https://paris-hotel.com", PhoneNumber = "555-2468" };
            accommodationService.AddAccommodation(accommodation);
            Debug.WriteLine("Accommodation added: " + accommodation.Name);

            var foundAccommodation = accommodationService.GetAccommodation(accommodation.Id);
            Debug.WriteLine("Accommodation found: " + foundAccommodation.Name);

            // Restaurant service test
            var restaurantService = new RestaurantService(dbContext);

            var restaurant = new Restaurant { Name = "French Bistro", Address = "456 Paris Avenue", Cuisine = "French", Website = "https://french-bistro.com", PhoneNumber = "555-3690" };
            restaurantService.AddRestaurant(restaurant);
            Debug.WriteLine("Restaurant added: " + restaurant.Name);

            var foundRestaurant = restaurantService.GetRestaurant(restaurant.Id);
            Debug.WriteLine("Restaurant found: " + foundRestaurant.Name);

            // Tourist attraction service test
            var attractionService = new TouristAttractionService(dbContext);

            var attraction = new TouristAttraction { Name = "Eiffel Tower", Address = "Eiffel Tower Address", Description = "Historic landmark in Paris", EntryFee = 25m, Website = "https://eiffel-tower.com" };
            attractionService.AddAttraction(attraction);
            Debug.WriteLine("Tourist attraction added: " + attraction.Name);

            var foundAttraction = attractionService.GetAttraction(attraction.Id);
            Debug.WriteLine("Tourist attraction found: " + foundAttraction.Name);
            // Booking service test
            var bookingService = new BookingService(dbContext);

            var booking = new Booking();

            booking.BookingDate = DateTime.Now;

            booking.Client = client;

            booking.Trip = trip;

            booking.IsPaid = false;

            bookingService.AddBooking(booking);
            Debug.WriteLine("Booking added for client: " + client.Name);

            var foundBooking = bookingService.GetBooking(booking.Id);
            Debug.WriteLine("Booking found for client: " + foundBooking.Client.Name);

            // Cleanup - remove created records
            bookingService.RemoveBooking(foundBooking);
            attractionService.RemoveAttraction(foundAttraction);
            restaurantService.RemoveRestaurant(foundRestaurant);
            accommodationService.RemoveAccommodation(foundAccommodation);
            tripService.RemoveTrip(foundTrip);
            agentService.RemoveAgent(foundAgent);
            clientService.RemoveClient(foundClient);

            Debug.WriteLine("All records removed.");
        }
    }
}
