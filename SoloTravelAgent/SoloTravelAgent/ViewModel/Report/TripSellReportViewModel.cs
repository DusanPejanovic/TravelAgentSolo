using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SoloTravelAgent.Help;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.DTO;
using SoloTravelAgent.Model.Service;
using SoloTravelAgent.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SoloTravelAgent.ViewModel.Report
{
    public class TripSellReportViewModel: ViewModelBase
    {
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

        private TripStatistics _selectedTripStatistics;
        public TripStatistics SelectedTripStatistics
        {
            get { return _selectedTripStatistics; }
            set
            {
                _selectedTripStatistics = value;
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

        public RelayCommand<int> ShowSoldBookingsCommand { get; }

        public RelayCommand<string> ShowHelpCommand { get; private set; }

        private readonly TripStatisticsService _tripStatisticsService;

        public TripSellReportViewModel() {
            var dbContext = new TravelSystemDbContext();
            _tripStatisticsService = new TripStatisticsService(dbContext);
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

            ShowSoldBookingsCommand = new RelayCommand<int>(ShowSoldBookings);
            ShowHelpCommand = new RelayCommand<string>(ShowHelpExecute);
        }

        private void ShowHelpExecute(string helpKey)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            HelpProvider.ShowHelp(helpKey, mainWindow);
        }

        public void ShowSoldBookings(int tripId)
        {
            NavigationService.Instance.NavigateTo(new SoldBookingsForTripViewModel(tripId));
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
            var tripStatistics = _tripStatisticsService.GetTripStatisticsForMonth(year, month);
            TripStatistics = new ObservableCollection<TripStatistics>(tripStatistics);
        }

        public void LoadDataForLastSixMonths()
        {
            var tripStatistics = _tripStatisticsService.GetTripStatisticsForLastMonths(6);
            TripStatistics = new ObservableCollection<TripStatistics>(tripStatistics);
        }

        public void LoadDataForAllTime()
        {
            var tripStatistics = _tripStatisticsService.GetTripStatisticsForAllTime();
            TripStatistics = new ObservableCollection<TripStatistics>(tripStatistics);
        }

        public void LoadDataForPopularTrips()
        {
            var tripStatistics = _tripStatisticsService.GetPopularTrips();
            TripStatistics = new ObservableCollection<TripStatistics>(tripStatistics);
        }

        public void LoadDataForProfitableTrips()
        {
            var tripStatistics = _tripStatisticsService.GetProfitableTrips();
            TripStatistics = new ObservableCollection<TripStatistics>(tripStatistics);
        }
    }
}
