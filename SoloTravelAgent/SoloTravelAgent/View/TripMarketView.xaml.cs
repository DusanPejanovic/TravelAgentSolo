using Microsoft.EntityFrameworkCore;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.View.DialogView;
using SoloTravelAgent.ViewModel;
using System;
using System.Collections.Generic;
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

namespace SoloTravelAgent.View
{
    /// <summary>
    /// Interaction logic for TripMarketView.xaml
    /// </summary>
    public partial class TripMarketView : Window
    {
        private readonly TripViewModel _viewModel;
        private readonly TravelSystemDbContext dbContext;
        public TripMarketView()
        {
            InitializeComponent();
            dbContext = new TravelSystemDbContext();
            _viewModel = new TripViewModel(dbContext);

            // Set the DataContext to the ViewModel instance
            DataContext = _viewModel;
        }

        private bool IsMaximize = false;
        private void ViewIt_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var trip = button.DataContext as Trip; // Assuming your trip model class is called Trip
                if (trip != null)
                {

                    var restaurantView = new RestaurantMarketView(trip);
                    restaurantView.Show();
                    this.Close();
                }
            }
        }


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


    }
}
