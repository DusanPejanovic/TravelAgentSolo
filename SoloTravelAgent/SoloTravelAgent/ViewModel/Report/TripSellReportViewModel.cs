using GalaSoft.MvvmLight;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.DTO;
using SoloTravelAgent.Model.Service;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SoloTravelAgent.ViewModel.Report
{
    public class TripSellReportViewModel: ViewModelBase
    {
        private readonly BookingService _bookingService;

        private ObservableCollection<TripStatistics> _tripStatistics;
        public ObservableCollection<TripStatistics> TripStatistics
        {
            get { return _tripStatistics; }
            set
            {
                _tripStatistics = value;
                RaisePropertyChanged(nameof(TripStatisticsCount));
                RaisePropertyChanged();
            }
        }

        public int TripStatisticsCount
        {
            get { return TripStatistics?.Count ?? 0; }
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

        public TripSellReportViewModel() {
            var dbContext = new TravelSystemDbContext();
            _bookingService = new BookingService(dbContext);
            _tripStatistics = new ObservableCollection<TripStatistics>();

            filters = new ObservableCollection<string>
            {
                "No Filter",
                "Popular Trips",
                "Profitable Trips",
                "This Month",
                "Last 6 Months",
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
                case "Popular Trips":
                    LoadDataForPopularTrips();
                    break;
                case "Profitable Trips":
                    LoadDataForProfitableTrips();
                    break;
                case "This Month":
                    var currentDate = DateTime.Now; 
                    var currentYear = currentDate.Year;
                    var currentMonth = currentDate.Month; 
                    LoadDataForMonth(currentYear, currentMonth); 
                    break;
                case "Last 6 Months":
                    LoadDataForLastSixMonths();
                    break;
            }

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                var filteredList = TripStatistics.Where(t => t.TripName.Contains(SearchQuery)
                                                             || t.TripId.ToString().StartsWith(SearchQuery)
                                                             || t.NumberOfBookings.ToString().StartsWith(SearchQuery)
                                                             || t.TotalMoneyEarned.ToString().StartsWith(SearchQuery)).ToList();
                TripStatistics = new ObservableCollection<TripStatistics>(filteredList);
            }
        }

        public void LoadDataForMonth(int year, int month)
        {
            var tripStatistics =  _bookingService.GetTripStatisticsForMonth(year, month);
            TripStatistics = new ObservableCollection<TripStatistics>(tripStatistics);
        }

        public void LoadDataForLastSixMonths()
        {
            var tripStatistics =  _bookingService.GetTripStatisticsForLastMonths(6);
            TripStatistics = new ObservableCollection<TripStatistics>(tripStatistics);
        }

        public void LoadDataForAllTime()
        {
            var tripStatistics =  _bookingService.GetTripStatisticsForAllTime();
            TripStatistics = new ObservableCollection<TripStatistics>(tripStatistics);
        }

        public void LoadDataForPopularTrips()
        {
            var tripStatistics = _bookingService.GetPopularTrips();
            TripStatistics = new ObservableCollection<TripStatistics>(tripStatistics);
        }

        public void LoadDataForProfitableTrips()
        {
            var tripStatistics = _bookingService.GetProfitableTrips();
            TripStatistics = new ObservableCollection<TripStatistics>(tripStatistics);
        }
    }
}
