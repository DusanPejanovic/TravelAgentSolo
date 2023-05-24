using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;

namespace SoloTravelAgent.ViewModel
{
    public class TripViewModel
    {
        private readonly TripService _tripService;

        public TripViewModel(TravelSystemDbContext dbContext)
        {
            _tripService = new TripService(dbContext);
            LoadTrips();
            AddTripCommand = new RelayCommand(_ => AddTrip(), _ => CanAddOrUpdateTrip());
            UpdateTripCommand = new RelayCommand(_ => UpdateTrip(), _ => CanAddOrUpdateTrip());
            DeleteTripCommand = new RelayCommand(_ => DeleteTrip(), _ => CanDeleteTrip());
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Trip> Trips { get; set; } = new ObservableCollection<Trip>();

        public Trip SelectedTrip { get; set; }

        public ICommand AddTripCommand { get; set; }

        public ICommand UpdateTripCommand { get; set; }

        public ICommand DeleteTripCommand { get; set; }


        public int TripCount
        {
            get
            {
                return Trips?.Count ?? 0;
            }
        }



        private void LoadTrips()
        {
            var trips = _tripService.GetAllTrips();
            Trips.Clear();
            foreach (var trip in trips)
            {
                Trips.Add(trip);
            }
        }

        private void AddTrip()
        {
            // Implement the details for adding a new trip
            // Similar to AddRestaurant(), you may need to collect multiple pieces of data
        }

        private void UpdateTrip()
        {
            if (SelectedTrip != null)
            {
                // Implement the details for updating a trip
                // Similar to UpdateRestaurant(), you may need to collect multiple pieces of data
            }
        }

        private void DeleteTrip()
        {
            if (SelectedTrip != null)
            {
                _tripService.RemoveTrip(SelectedTrip);
                LoadTrips();
            }
        }

        private bool CanAddOrUpdateTrip()
        {
            // Add your logic to determine if Add or Update buttons should be enabled
            return true;
        }

        private bool CanDeleteTrip()
        {
            // Add your logic to determine if the Delete button should be enabled
            return SelectedTrip != null;
        }
    }

}
