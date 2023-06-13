using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.VisualBasic;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;

namespace SoloTravelAgent.ViewModel
{

    public class AccommodationsViewModel : INotifyPropertyChanged
    {
        private readonly AccommodationService _accommodationService;
        private readonly TripService _tripService;
        private Trip _selectedTrip;
        public AccommodationsViewModel(Trip selectedTrip)
        {
            var dbContext = new TravelSystemDbContext();
            _accommodationService = new AccommodationService(dbContext);
            _tripService = new TripService(dbContext);
            _selectedTrip = selectedTrip;
            LoadAccommodations();
            AddAccommodationCommand = new RelayCommand<Accommodation>(accommodation => AddAccommodation(accommodation), _ => CanAddOrUpdateAccommodation());
            UpdateAccommodationCommand = new RelayCommand(_ => UpdateAccommodation(), _ => CanAddOrUpdateAccommodation());
            DeleteAccommodationCommand = new RelayCommand(_ => DeleteAccommodation(), _ => CanDeleteAccommodation());
        }

        public ObservableCollection<Accommodation> Accommodations { get; set; } = new ObservableCollection<Accommodation>();

        public Accommodation SelectedAccommodation { get; set; }

        public RelayCommand<Accommodation> AddAccommodationCommand { get; set; }

        public ICommand UpdateAccommodationCommand { get; set; }

        public ICommand DeleteAccommodationCommand { get; set; }

        public void LoadAccommodations()
        {
            if (_selectedTrip != null)
            {
                var trip = _tripService.GetTrip(_selectedTrip.Id);
                Accommodations.Clear();
                foreach (var accommodation in trip.Accommodations)
                {
                    Accommodations.Add(accommodation);
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

        public int AccommodationCount
        {
            get
            {
                return _selectedTrip?.Accommodations.Count ?? 0;
            }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(searchText));
                OnPropertyChanged(nameof(IsSearchEmpty));
            }
        }

        public bool IsSearchEmpty
        {
            get { return string.IsNullOrEmpty(SearchText); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public void AddAccommodation(Accommodation accommodation)
        {
            var trip = _tripService.GetTrip(SelectedTrip.Id);

            trip.Accommodations?.Add(accommodation);
            _accommodationService.AddAccommodation(accommodation);
            _tripService.UpdateTrip(trip);
            SelectedTrip = trip;
            LoadAccommodations();
            OnPropertyChanged(nameof(AccommodationCount));
   
        }

        public void UpdateAccommodation()
        {
            if (SelectedAccommodation != null)
            {
                _accommodationService.UpdateAccommodation(SelectedAccommodation);
                LoadAccommodations();
               
            }
        }

        private void DeleteAccommodation()
        {
            if (SelectedAccommodation != null)
            {
                SelectedTrip.Accommodations.Remove(SelectedAccommodation);
                _accommodationService.RemoveAccommodation(SelectedAccommodation);
                LoadAccommodations();

                OnPropertyChanged(nameof(AccommodationCount));
            }
        }

        private bool CanAddOrUpdateAccommodation()
        {
            // Add your logic to determine if Add or Update buttons should be enabled
            return true;
        }

        private bool CanDeleteAccommodation()
        {
            // Add your logic to determine if the Delete button should be enabled
            return SelectedAccommodation != null;
        }
    }
}

