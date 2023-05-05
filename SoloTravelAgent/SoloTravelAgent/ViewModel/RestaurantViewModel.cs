using Microsoft.VisualBasic;
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
  
            public RestaurantViewModel(TravelSystemDbContext dbContext)
        {
            _restaurantService = new RestaurantService(dbContext);
            LoadRestaurants();
            AddRestaurantCommand = new RelayCommand(_ => AddRestaurant(), _ => CanAddOrUpdateRestaurant());
            UpdateRestaurantCommand = new RelayCommand(_ => UpdateRestaurant(), _ => CanAddOrUpdateRestaurant());
            DeleteRestaurantCommand = new RelayCommand(_ => DeleteRestaurant(), _ => CanDeleteRestaurant());
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

        private void AddRestaurant()
        {
            string name = Interaction.InputBox("Enter restaurant name:", "Add Restaurant");
            if (!string.IsNullOrEmpty(name))
            {
                Restaurant newRestaurant = new Restaurant { Name = name };
                _restaurantService.AddRestaurant(newRestaurant);
                LoadRestaurants();
            }
        }

        private void UpdateRestaurant()
        {
            if (SelectedRestaurant != null)
            {
                string name = Interaction.InputBox("Enter new restaurant name:", "Update Restaurant", SelectedRestaurant.Name);
                if (!string.IsNullOrEmpty(name))
                {
                    SelectedRestaurant.Name = name;
                    _restaurantService.UpdateRestaurant(SelectedRestaurant);
                    LoadRestaurants();
                }
            }
        }

        private void DeleteRestaurant()
        {
            if (SelectedRestaurant != null)
            {
                _restaurantService.RemoveRestaurant(SelectedRestaurant);
                LoadRestaurants();
            }
        }

        private bool CanAddOrUpdateRestaurant()
        {
            // Add your logic to determine if Add or Update buttons should be enabled
            return true;
        }

        private bool CanDeleteRestaurant()
        {
            // Add your logic to determine if the Delete button should be enabled
            return SelectedRestaurant != null;
        }
    }
}
