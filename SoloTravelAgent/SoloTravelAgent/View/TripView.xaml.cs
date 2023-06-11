using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.View.DialogView;
using SoloTravelAgent.View.DialogView.StepperView;
using SoloTravelAgent.ViewModel;

namespace SoloTravelAgent.View
{
    /// <summary>
    /// Interaction logic for TripView.xaml
    /// </summary>
    public partial class TripView : Window
    {
        private readonly TripViewModel _viewModel;
        private readonly TravelSystemDbContext dbContext;
        public TripView()
        {
            InitializeComponent();
            dbContext = new TravelSystemDbContext();
            _viewModel = new TripViewModel(dbContext);

            // Set the DataContext to the ViewModel instance
            DataContext = _viewModel;

        }


        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void ComboBox_MouseEnter(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            comboBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C88EA7"));

        }

        private void ComboBox_MouseLeave(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            comboBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#917FB3"));

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SoloTravelAgent.View.DialogView.StepperView.AddTripDialogView(_viewModel, dbContext);
            dialog.Owner = this;
            dialog.ShowDialog();

        }


        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var trip = (sender as Button).DataContext as Trip;
            if (trip == null) return;
            var dialog = new EditTripView(trip, _viewModel);
            dialog.Owner = this; // ovo mi je roditelj prozor
            dialog.ShowDialog();

        }
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var trip = (sender as Button).DataContext as Trip;
            if (trip == null) return;

            var msg = $"Are you sure you want to delete the tourist attraction: {trip.Name}?";
            var result = MessageBox.Show(msg, "Confirm Delete", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _viewModel.DeleteTripCommand.Execute(trip);
            }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedTrip = (sender as DataGrid)?.SelectedItem as Trip;

            if (selectedTrip != null)
            {
                var restaurantView = new RestaurantView(selectedTrip);
                restaurantView.Show();
                this.Close();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            if (selectedItem != null && _viewModel != null)
            {
                string selectedContent = selectedItem.Content.ToString();

                if (selectedContent == "Price >= 1500")
                {
                    List<Trip> filteredTrips = FilterPriceGreaterThanOrEqualTo1500();
                    UpdateTripsCollection(filteredTrips);
                }
                else if (selectedContent == "Price < 1500")
                {
                    List<Trip> filteredTrips = FilterPriceLowerThan1500();
                    UpdateTripsCollection(filteredTrips);
                }
                else if (selectedContent == "Next Week")
                {
                    List<Trip> filteredTrips = FilterTripsNextWeek();
                    UpdateTripsCollection(filteredTrips);
                }
                else if (selectedContent == "This Month")
                {
                    List<Trip> filteredTrips = FilterTripsThisMonth();
                    UpdateTripsCollection(filteredTrips);
                }
                else if (selectedContent == "Next Month")
                {
                    List<Trip> filteredTrips = FilterTripsNextMonth();
                    UpdateTripsCollection(filteredTrips);
                }
                else if (selectedContent == "No Filter")
                {
                    _viewModel.LoadTrips();
                }
            }
        }

        private List<Trip> FilterTripsThisMonth()
        {
            _viewModel.LoadTrips();
            var trips = _viewModel.Trips;
            var today = DateTime.Today;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            return trips.Where(trip => trip.StartDate >= firstDayOfMonth && trip.StartDate <= lastDayOfMonth).ToList();
        }

        private List<Trip> FilterTripsNextMonth()
        {
            _viewModel.LoadTrips();
            var trips = _viewModel.Trips;
            var today = DateTime.Today;
            var firstDayOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);
            var lastDayOfNextMonth = firstDayOfNextMonth.AddMonths(1).AddDays(-1);
            return trips.Where(trip => trip.StartDate >= firstDayOfNextMonth && trip.StartDate <= lastDayOfNextMonth).ToList();
        }


        private List<Trip> FilterTripsNextWeek()
        {
            _viewModel.LoadTrips();
            var trips = _viewModel.Trips;
            DateTime today = DateTime.Today;
            DateTime oneWeekFromNow = today.AddDays(7);
            return trips.Where(trip => trip.StartDate >= today && trip.StartDate <= oneWeekFromNow).ToList();
        }


        private void UpdateTripsCollection(List<Trip> newTrips)
        {
            _viewModel.Trips.Clear();
            foreach (var trip in newTrips)
            {
                _viewModel.Trips.Add(trip);
            }
        }



        private List<Trip> FilterPriceGreaterThanOrEqualTo1500()
        {
            _viewModel.LoadTrips();
            var trips = _viewModel.Trips;
            return trips.Where(trip => trip.Price >= 1500).ToList();
        }

        private List<Trip> FilterPriceLowerThan1500()
        {
            _viewModel.LoadTrips();
            var trips = _viewModel.Trips; 
            return trips.Where(trip => trip.Price < 1500).ToList();
        }


    }
}
