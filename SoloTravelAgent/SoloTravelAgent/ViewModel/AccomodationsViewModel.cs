using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

    public class AccommodationsViewModel : INotifyPropertyChanged
    {
        private readonly AccommodationService _accommodationService;
        private Trip _selectedTrip;
        public AccommodationsViewModel(TravelSystemDbContext dbContext, Trip selectedTrip)
        {
            _accommodationService = new AccommodationService(dbContext);
            _selectedTrip = selectedTrip;
            LoadAccommodations();
            AddAccommodationCommand = new RelayCommand(_ => AddAccommodation(), _ => CanAddOrUpdateAccommodation());
            UpdateAccommodationCommand = new RelayCommand(_ => UpdateAccommodation(), _ => CanAddOrUpdateAccommodation());
            DeleteAccommodationCommand = new RelayCommand(_ => DeleteAccommodation(), _ => CanDeleteAccommodation());
        }

        public ObservableCollection<Accommodation> Accommodations { get; set; } = new ObservableCollection<Accommodation>();

        public Accommodation SelectedAccommodation { get; set; }

        public ICommand AddAccommodationCommand { get; set; }

        public ICommand UpdateAccommodationCommand { get; set; }

        public ICommand DeleteAccommodationCommand { get; set; }

        private void LoadAccommodations()
        {
            if (_selectedTrip != null)
            {
                Accommodations.Clear();
                foreach (var accommodation in _selectedTrip.Accommodations)
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



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private void AddAccommodation()
        {
            string name = Interaction.InputBox("Enter accommodation name:", "Add Accommodation");
            if (!string.IsNullOrEmpty(name))
            {
                Accommodation newAccommodation = new Accommodation { Name = name };
                _accommodationService.AddAccommodation(newAccommodation);
                LoadAccommodations();
            }
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

