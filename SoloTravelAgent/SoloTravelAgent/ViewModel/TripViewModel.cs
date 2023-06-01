﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;

namespace SoloTravelAgent.ViewModel
{
    public class TripViewModel : INotifyPropertyChanged
    {
        private readonly TripService _tripService;

        public TripViewModel(TravelSystemDbContext dbContext)
        {
            _tripService = new TripService(dbContext);
            LoadTrips();
            AddTripCommand = new RelayCommand<Trip>(trip => AddTrip(trip), _ => CanAddOrUpdateTrip());
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
            // Add your logic to determine if Add or Update buttons should be enabled
            return true;
        }

        private bool CanDeleteTrip()
        {
            // Add your logic to determine if the Delete button should be enabled
            return SelectedTrip != null;
        }

     

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}