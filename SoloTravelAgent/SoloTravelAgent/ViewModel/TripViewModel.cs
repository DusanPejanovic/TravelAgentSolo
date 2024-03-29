﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SoloTravelAgent.Help;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;

namespace SoloTravelAgent.ViewModel
{
    public class TripViewModel : INotifyPropertyChanged
    {
        private readonly TripService _tripService;
        private string _searchText;
        private bool _isSearchEmpty = true;
        private ICollectionView _filteredTrips;

        public RelayCommand<string> ShowHelpCommand { get; private set; }

        public TripViewModel()
        {
            var dbContext = new TravelSystemDbContext();
            Trips = new ObservableCollection<Trip>();
            _tripService = new TripService(dbContext);
            LoadTrips();
            AddTripCommand = new RelayCommand<Trip>(trip => AddTrip(trip), _ => CanAddOrUpdateTrip());
            UpdateTripCommand = new RelayCommand(_ => UpdateTrip(), _ => CanAddOrUpdateTrip());
            DeleteTripCommand = new RelayCommand(_ => DeleteTrip(), _ => CanDeleteTrip());
            SearchText = string.Empty;

            ShowHelpCommand = new RelayCommand<string>(ShowHelpExecute);
        }

        private void ShowHelpExecute(string helpKey)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            HelpProvider.ShowHelp(helpKey, mainWindow);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Trip> Trips { get; set; } = new ObservableCollection<Trip>();

        public Trip SelectedTrip { get; set; }

        public ICommand AddTripCommand { get; set; }

        public ICommand UpdateTripCommand { get; set; }

        public ICommand DeleteTripCommand { get; set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                IsSearchEmpty = string.IsNullOrEmpty(value);
                FilterCollection();
            }
        }

        public bool IsSearchEmpty
        {
            get { return _isSearchEmpty; }
            set
            {
                _isSearchEmpty = value;
                OnPropertyChanged(nameof(IsSearchEmpty));
            }
        }

        public int TripCount
        {
            get
            {
                return Trips?.Count ?? 0;
            }       
        }


        public void LoadTrips()
        {
            var trips = _tripService.GetAllTrips();
            Trips.Clear();
            foreach (var trip in trips)
            {
                Trips.Add(trip);
            }
           
            OnPropertyChanged(nameof(TripCount));
        
        }

        public void AddTrip(Trip trip)
        {
            _tripService.AddTrip(trip);
            LoadTrips();
            OnPropertyChanged(nameof(TripCount));
        }

        public void UpdateTrip()
        {
            if (SelectedTrip != null)
            {
                _tripService.UpdateTrip(SelectedTrip);
                LoadTrips();
            }
        }
        public void UpdateT(Trip t)
        {
            if (SelectedTrip != null)
            {
                _tripService.UpdateTrip(t);
                LoadTrips();
            }
        }


        private void DeleteTrip()
        {
            if (SelectedTrip != null)
            {
                _tripService.RemoveTrip(_tripService.GetTrip(SelectedTrip.Id));
                LoadTrips();
                OnPropertyChanged(nameof(TripCount));
            }
        }

        private bool CanAddOrUpdateTrip()
        {
          
            return true;
        }

        private bool CanDeleteTrip()
        {
           
            return SelectedTrip != null;
        }
        public ICollectionView FilteredTrips
        {
            get
            {
                if (_filteredTrips == null)
                {
                    _filteredTrips = CollectionViewSource.GetDefaultView(Trips);
                    _filteredTrips.Filter = FilterTrips;
                }
                return _filteredTrips;
            }
        }
        private bool FilterTrips(object obj)
        {
            var trip = obj as Trip;
            if (trip == null) return false;

            // Return trips where Name starts with SearchText
            return trip.Name.StartsWith(SearchText, StringComparison.OrdinalIgnoreCase);
        }

        private void FilterCollection()
        {
            FilteredTrips.Refresh();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
