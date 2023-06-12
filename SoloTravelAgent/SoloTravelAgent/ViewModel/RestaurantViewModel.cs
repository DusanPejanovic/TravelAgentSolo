using GalaSoft.MvvmLight.Command;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SoloTravelAgent.ViewModel
{

    public class RestaurantViewModel : INotifyPropertyChanged
    {
        private readonly RestaurantService _restaurantService;
        private readonly TripService _tripService;  
        private Trip _selectedTrip;
        private int _numberOfRestaurants;
  
        public RestaurantViewModel(Trip selectedTrip)
        {
            var dbContext = new TravelSystemDbContext();
            _restaurantService = new RestaurantService(dbContext);
            _tripService = new TripService(dbContext);
            _selectedTrip = selectedTrip;   
            LoadRestaurants();
            AddRestaurantCommand = new RelayCommand<Restaurant>(restaurant => AddRestaurant(restaurant), _ => CanAddOrUpdateRestaurant());
            UpdateRestaurantCommand = new RelayCommand(_ => UpdateRestaurant(), _ => CanAddOrUpdateRestaurant());
            DeleteRestaurantCommand = new RelayCommand(_ => DeleteRestaurant(), _ => CanDeleteRestaurant());
        }

        public ObservableCollection<Restaurant> Restaurants { get; set; } = new ObservableCollection<Restaurant>();

        public Restaurant SelectedRestaurant { get; set; }

        public RelayCommand<Restaurant> AddRestaurantCommand { get; set; }


        public ICommand UpdateRestaurantCommand { get; set; }

        public ICommand DeleteRestaurantCommand { get; set; }

        public void LoadRestaurants()
        {   
            if (_selectedTrip != null)
            {
                var trip = _tripService.GetTrip(_selectedTrip.Id);
                Restaurants.Clear();
                foreach (var restaurant in trip.Restaurants) 
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
                return SelectedTrip?.Restaurants.Count ?? 0;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            var trip = _tripService.GetTrip(SelectedTrip.Id);
            
            trip.Restaurants?.Add(restaurant);
            _restaurantService.AddRestaurant(restaurant);
            _tripService.UpdateTrip(trip);
            SelectedTrip = trip;
            LoadRestaurants();
            OnPropertyChanged(nameof(RestaurantCount));
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
            var trip = _tripService.GetTrip(SelectedTrip.Id);
            trip.Restaurants.Remove(SelectedRestaurant);
            SelectedTrip = trip;
            _tripService.UpdateTrip(trip);
            _restaurantService.RemoveRestaurant(SelectedRestaurant);
            LoadRestaurants();
            OnPropertyChanged(nameof(RestaurantCount));
            
        }

        private bool CanAddOrUpdateRestaurant()
        {
            return true;
        }


        private bool CanDeleteRestaurant()
        { 
            return SelectedRestaurant != null;
        }
    }
}
