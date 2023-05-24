using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.VisualBasic;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;

namespace SoloTravelAgent.ViewModel
{
    public class TouristAttractionsViewModel : INotifyPropertyChanged
    {
        private readonly TouristAttractionService _touristAttractionService;
        private Trip _selectedTrip;

        public TouristAttractionsViewModel(TravelSystemDbContext dbContext, Trip selectedTrip)
        {
            _selectedTrip = selectedTrip;
            _touristAttractionService = new TouristAttractionService(dbContext);
            LoadTouristAttractions();
            AddTouristAttractionCommand = new RelayCommand(_ => AddTouristAttraction(), _ => CanAddOrUpdateTouristAttraction());
            UpdateTouristAttractionCommand = new RelayCommand(_ => UpdateTouristAttraction(), _ => CanAddOrUpdateTouristAttraction());
            DeleteTouristAttractionCommand = new RelayCommand(_ => DeleteTouristAttraction(), _ => CanDeleteTouristAttraction());
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
                }
            }
        }

        public int AttractionCount
        {
            get
            {
                return _selectedTrip?.TouristAttractions.Count ?? 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void LoadTouristAttractions()
        {
        
            if (_selectedTrip != null)
            {
                
                TouristAttractions.Clear();
                Debug.WriteLine(_selectedTrip.Name + "ovdjeeee si");
                Debug.WriteLine(_selectedTrip.TouristAttractions.Count());
                foreach (var attraction in _selectedTrip.TouristAttractions)
                {
                    TouristAttractions.Add(attraction);
                }
            }
        }

        private void AddTouristAttraction()
        {
            string name = Interaction.InputBox("Enter tourist attraction name:", "Add Tourist Attraction");
            if (!string.IsNullOrEmpty(name))
            {
                TouristAttraction newAttraction = new TouristAttraction { Name = name };
                _touristAttractionService.AddAttraction(newAttraction);
                LoadTouristAttractions();
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
                SelectedTrip.TouristAttractions.Remove(SelectedTouristAttraction);
                _touristAttractionService.RemoveAttraction(SelectedTouristAttraction);
                LoadTouristAttractions();
                OnPropertyChanged(nameof(AttractionCount));
            }
        }

        private bool CanAddOrUpdateTouristAttraction()
        {
            // Add your logic to determine if Add or Update buttons should be enabled
            return true;
        }

        private bool CanDeleteTouristAttraction()
        {
            // Add your logic to determine if the Delete button should be enabled
            return SelectedTouristAttraction != null;
        }
    }
}
