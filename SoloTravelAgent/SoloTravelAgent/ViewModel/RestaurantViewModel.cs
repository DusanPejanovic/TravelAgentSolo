using Microsoft.VisualBasic;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SoloTravelAgent.ViewModel
{

    public class RestaurantViewModel : INotifyPropertyChanged
    {
        private readonly RestaurantService _restaurantService;
        private Trip _selectedTrip;
        private int _numberOfRestaurants;
  
            public RestaurantViewModel(TravelSystemDbContext dbContext, Trip selectedTrip)
        {
            _restaurantService = new RestaurantService(dbContext);
            _selectedTrip = selectedTrip;   
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
            if (_selectedTrip != null)
            {
                Debug.WriteLine("sad sam ovdje");
                Restaurants.Clear();
                foreach (var restaurant in _selectedTrip.Restaurants) 
                {
                    Restaurants.Add(restaurant);
                }
            }
        }

        public Trip SelectedTrip
        {
            get => _selectedTrip;
            set
            {
                if (_selectedTrip != value)
                {
                    _selectedTrip = value;
                    OnPropertyChanged(nameof(SelectedTrip));
                }
            }
        }


        public int RestaurantCount
        {
            get
            {
                return _selectedTrip?.Restaurants.Count ?? 0;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public void UpdateRestaurant()
        {
            if (SelectedRestaurant != null)
            {
                _restaurantService.UpdateRestaurant(SelectedRestaurant);
                LoadRestaurants();
            }
        }

        private void DeleteRestaurant()
        {
           
            SelectedTrip.Restaurants.Remove(SelectedRestaurant);
            _restaurantService.RemoveRestaurant(SelectedRestaurant);
            LoadRestaurants();

            OnPropertyChanged(nameof(RestaurantCount));
            
        }


        private bool CanAddOrUpdateRestaurant()
        {
            // Add your logic to determine if Add or Update buttons should be enabled
            return true;
        }

        private bool CanDeleteRestaurant()
        { 
            return SelectedRestaurant != null;
        }
    }
}
