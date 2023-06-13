using GalaSoft.MvvmLight;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;
using SoloTravelAgent.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace SoloTravelAgent.ViewModel
{
    public class TripBookedViewModel : ViewModelBase
    {
        private readonly ClientService _clientService;

        private ObservableCollection<Booking> _pendingBookings;
        public ObservableCollection<Booking> PendingBookings
        {
            get { return _pendingBookings; }
            set
            {
                _pendingBookings = value;
                RaisePropertyChanged(nameof(PendingBookingsCount));
                RaisePropertyChanged();
            }
        }

        public int PendingBookingsCount
        {
            get { return PendingBookings?.Count ?? 0; }
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

        public bool IsSearchEmpty
        {
            get { return string.IsNullOrEmpty(SearchQuery); }
        }

        private readonly BookingService _bookingService;

        public TripBookedViewModel()
        {
            var dbContext = new TravelSystemDbContext();
            _bookingService = new BookingService(dbContext);
            _clientService = new ClientService(dbContext);

            filters = new ObservableCollection<string>
            {
                "No Filter",
                "This Week",
                "This Month",
            };
            SelectedFilter = filters.First();
        }

        public void ApproveBooking(int id)
        {
            _bookingService.ApproveBooking(id);
            LoadDataForCurrentFilter();
        }

        public void DeleteBooking(int id)
        {
            _bookingService.DeleteBooking(id);
            LoadDataForCurrentFilter();
        }

        private void FilterData()
        {
            LoadDataForCurrentFilter();

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                Client currentClient = _clientService.GetClient(AuthenticationManager.CurrentUser.Id);
                searchQuery = searchQuery.ToLower();
                var filteredList = PendingBookings.Where(b => (b.Id.ToString().Contains(SearchQuery)
                                                     || b.Client.Email.ToLower().StartsWith(SearchQuery)
                                                     || b.Trip.Name.ToLower().StartsWith(SearchQuery)
                                                     || b.Trip.Price.ToString().StartsWith(SearchQuery)
                                                     || b.BookingDate.ToString("dd-MM-yyyy").StartsWith(SearchQuery))
                                                     && (b.Client.Id == currentClient.Id)).ToList();


                PendingBookings = new ObservableCollection<Booking>(filteredList);
            }
        }

        public void LoadDataForCurrentFilter()
        {
            switch (selectedFilter)
            {
                case "No Filter":
                    LoadDataForAllTime();
                    break;
                case "This Week":
                    LoadDataForCurrentWeek();
                    break;
                case "This Month":
                    LoadDataForCurrentMonth();
                    break;
            }
        }

        public void LoadDataForAllTime()
        {
            var pendingBookings = _bookingService.GetUnpaidBookings2();

            PendingBookings = new ObservableCollection<Booking>(pendingBookings);
        }

        public void LoadDataForCurrentWeek()
        {
            var pendingBookings = _bookingService.GetUnpaidBookingsThisWeek2();
            pendingBookings = PendingBookings.Where(b => b.Client.Id == AuthenticationManager.CurrentUser.Id).ToList();
            PendingBookings = new ObservableCollection<Booking>(pendingBookings);
        }

        public void LoadDataForCurrentMonth()
        {
            var pendingBookings = _bookingService.GetUnpaidBookingsThisMonth2();
            pendingBookings = PendingBookings.Where(b => b.Client.Id == AuthenticationManager.CurrentUser.Id).ToList();

            PendingBookings = new ObservableCollection<Booking>(pendingBookings);
        }
    }
}
