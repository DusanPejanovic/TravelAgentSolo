using GalaSoft.MvvmLight;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.DTO;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SoloTravelAgent.ViewModel.Report
{
    public class SoldBookingsForTripViewModel: ViewModelBase
    {
        private ObservableCollection<Booking> _soldBookings;
        public ObservableCollection<Booking> SoldBookings
        {
            get { return _soldBookings; }
            set
            {
                _soldBookings = value;
                RaisePropertyChanged(nameof(SoldBookingsCount));
                RaisePropertyChanged();
            }
        }

        private TripStatistics _selectedBooking;
        public TripStatistics SelectedBooking
        {
            get { return _selectedBooking; }
            set
            {
                _selectedBooking = value;
                RaisePropertyChanged();
            }
        }

        public int SoldBookingsCount
        {
            get { return SoldBookings?.Count ?? 0; }
        }

        private ObservableCollection<string> filters;
        public ObservableCollection<string> Filters
        {
            get { return filters; }
            set
            {
                filters = value;
                RaisePropertyChanged();
            }
        }

        private string selectedFilter;
        public string SelectedFilter
        {
            get { return selectedFilter; }
            set
            {
                selectedFilter = value;
                RaisePropertyChanged();
                FilterData();
            }
        }

        private string searchQuery;
        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                searchQuery = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsSearchEmpty));
                FilterData();
            }
        }

        private Trip _selectedTrip;
        public Trip SelectedTrip
        {
            get { return _selectedTrip; }
            set
            {
                _selectedTrip = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSearchEmpty
        {
            get { return string.IsNullOrEmpty(SearchQuery); }
        }

        private readonly TripService _tripService;
        private readonly BookingService _bookingService;

        public SoldBookingsForTripViewModel(int selectedTripId) {
            var dbContext = new TravelSystemDbContext();
            _tripService = new TripService(dbContext);
            _bookingService = new BookingService(dbContext);
            SelectedTrip = _tripService.GetTrip(selectedTripId);

            filters = new ObservableCollection<string>
            {
                "No Filter",
                "This Month",
                "Previous 3 Months",
            };
            SelectedFilter = filters.First();
        }

        private void FilterData()
        {
            switch (selectedFilter)
            {
                case "No Filter":
                    LoadDataForAllTime();
                    break;
                case "This Month":
                    LoadDataForCurrentMonth();
                    break;
                case "Previous 3 Months":
                    LoadDataForLastSixMonths();
                    break;
            }

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                searchQuery = searchQuery.ToLower();
                var filteredList = SoldBookings.Where(b => b.Id.ToString().Contains(SearchQuery)
                                                     || b.Client.Email.ToLower().StartsWith(SearchQuery)
                                                     || b.Client.Name.ToLower().StartsWith(SearchQuery)
                                                     || b.Client.PhoneNumber.ToString().StartsWith(SearchQuery)
                                                     || b.BookingDate.ToString("dd-MM-yyyy").StartsWith(SearchQuery)).ToList();
                SoldBookings = new ObservableCollection<Booking>(filteredList);
            }
        }

        public void LoadDataForAllTime()
        {
            var soldBookings = _bookingService.GetBookingsByTripId(_selectedTrip.Id);
            SoldBookings = new ObservableCollection<Booking>(soldBookings);
        }

        public void LoadDataForCurrentMonth()
        {
            var soldBookings = _bookingService.GetCurrentMonthBookingsForTrip(_selectedTrip.Id);
            SoldBookings = new ObservableCollection<Booking>(soldBookings);
        }

        public void LoadDataForLastSixMonths()
        {
            var soldBookings = _bookingService.GetBookingsForPreviousMonthsAndTrip(3, _selectedTrip.Id);
            SoldBookings = new ObservableCollection<Booking>(soldBookings);
        }
    }
}
