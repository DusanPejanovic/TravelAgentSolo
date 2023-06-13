using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.VisualBasic;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;
using System.Windows.Data;
using System.Globalization;

namespace SoloTravelAgent.ViewModel
{
    public class TouristAttractionsViewModel : INotifyPropertyChanged
    {
        private readonly TouristAttractionService _touristAttractionService;
        private readonly TripService _tripService;
        private Trip _selectedTrip;
        private ICollectionView _touristAttractionsView;
        public ICollectionView TouristAttractionsView
        {
            get { return _touristAttractionsView; }
            set
            {
                _touristAttractionsView = value;
                OnPropertyChanged(nameof(TouristAttractionsView));
            }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                TouristAttractionsView.Refresh(); // Refresh the view to apply filters.
            }
        }

        public TouristAttractionsViewModel(Trip selectedTrip)
        {
            var dbContext = new TravelSystemDbContext();
            _selectedTrip = selectedTrip;
            _tripService = new TripService(dbContext);
            _touristAttractionService = new TouristAttractionService(dbContext);

            TouristAttractionsView = CollectionViewSource.GetDefaultView(TouristAttractions);
            TouristAttractionsView.Filter = FilterTouristAttractions;
            LoadTouristAttractions();

            AddTouristAttractionCommand = new RelayCommand<TouristAttraction>(attraction => AddTouristAttraction(attraction), _ => CanAddOrUpdateTouristAttraction());
            UpdateTouristAttractionCommand = new RelayCommand(_ => UpdateTouristAttraction(), _ => CanAddOrUpdateTouristAttraction());
            DeleteTouristAttractionCommand = new RelayCommand(_ => DeleteTouristAttraction(), _ => CanDeleteTouristAttraction());
        
        
        }
        private bool FilterTouristAttractions(object item)
        {
            if (item is TouristAttraction touristAttraction)
            {
                return string.IsNullOrEmpty(SearchText) || touristAttraction.Name.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public ObservableCollection<TouristAttraction> TouristAttractions { get; set; } = new ObservableCollection<TouristAttraction>();

        public TouristAttraction SelectedTouristAttraction { get; set; }

        public ICommand AddTouristAttractionCommand { get; set; }

        public ICommand UpdateTouristAttractionCommand { get; set; }

        public ICommand DeleteTouristAttractionCommand { get; set; }

        public Trip SelectedTrip
        {
            get => _selectedTrip;
            set
            {
                if (_selectedTrip != value)
                {
                    _selectedTrip = value;
                    OnPropertyChanged(nameof(SelectedTrip));
                    LoadTouristAttractions();
                }
            }
        }

        public int AttractionCount
        {
            get
            {
                return _selectedTrip?.TripTouristAttractions.Count ?? 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadTouristAttractions()
        {
            if (_selectedTrip != null)
            {
                TouristAttractions.Clear();

                foreach (var tripTouristAttraction in _selectedTrip.TripTouristAttractions)
                {
                    var attraction = tripTouristAttraction.TouristAttraction;
                    TouristAttractions.Add(attraction);
                }

                // Ensure that the view gets refreshed when new data is loaded.
                TouristAttractionsView.Refresh();
            }
        }


        public void AddTouristAttraction(TouristAttraction touristAttraction)
        {
            if (touristAttraction != null && _selectedTrip != null)
            {
                var trip = _tripService.GetTrip(_selectedTrip.Id);
                var existingAttraction = trip.TripTouristAttractions.FirstOrDefault(tta => tta.TouristAttraction == touristAttraction);
                if (existingAttraction != null)
                {
                    Debug.WriteLine("Attraction is already associated with the trip.");
                    return;
                }

                var tripTouristAttraction = new TripTouristAttraction
                {
                    Trip = trip,
                    TouristAttraction = touristAttraction
                };

                trip.TripTouristAttractions.Add(tripTouristAttraction);
                
                _tripService.UpdateTrip(trip);
                SelectedTrip = trip;
                LoadTouristAttractions();
                OnPropertyChanged(nameof(AttractionCount));
            }
        }

        public void UpdateTouristAttraction()
        {
            if (SelectedTouristAttraction != null)
            {
                _touristAttractionService.UpdateAttraction(SelectedTouristAttraction);
                LoadTouristAttractions();
            }
        }

        private void DeleteTouristAttraction()
        {
            if (SelectedTouristAttraction != null)
            {
                foreach (var tripTouristAttraction in _selectedTrip.TripTouristAttractions)
                {
                    if (tripTouristAttraction.TouristAttraction == SelectedTouristAttraction)
                    {
                        _selectedTrip.TripTouristAttractions.Remove(tripTouristAttraction);
                        break;
                    }
                }

                _touristAttractionService.RemoveAttraction(SelectedTouristAttraction);
                LoadTouristAttractions();
                OnPropertyChanged(nameof(AttractionCount));
            }
        }

        private bool CanAddOrUpdateTouristAttraction()
        { 
            return true;
        }

        private bool CanDeleteTouristAttraction()
        {
            return SelectedTouristAttraction != null;
        }
    }
}