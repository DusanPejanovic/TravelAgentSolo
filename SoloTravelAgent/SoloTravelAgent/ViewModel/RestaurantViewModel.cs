using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SoloTravelAgent.ViewModel
{
    public class RestaurantViewModel
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantViewModel() : this(new TravelSystemDbContext())
        {
        }
        public RestaurantViewModel(TravelSystemDbContext dbContext)
        {
            _restaurantService = new RestaurantService(dbContext);
            LoadRestaurants();
        }

        public ObservableCollection<Restaurant> Restaurants { get; set; } = new ObservableCollection<Restaurant>();

        public Restaurant SelectedRestaurant { get; set; }

        public ICommand AddRestaurantCommand { get; set; }

        public ICommand UpdateRestaurantCommand { get; set; }

        public ICommand DeleteRestaurantCommand { get; set; }

        private void LoadRestaurants()
        {
            var restaurants = _restaurantService.GetAllRestaurants();
            Restaurants.Clear();
            foreach (var restaurant in restaurants)
            {
                Restaurants.Add(restaurant);
            }
        }
    }
}
