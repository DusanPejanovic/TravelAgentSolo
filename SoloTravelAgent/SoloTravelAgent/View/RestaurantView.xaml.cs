using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.View.DialogView;
using SoloTravelAgent.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace SoloTravelAgent.View
{
    /// <summary>
    /// Interaction logic for RestaurantView.xaml
    /// </summary>
    public partial class RestaurantView : Window
    {

        private readonly RestaurantViewModel _viewModel;
        public Trip _selectedTrip;
        public RestaurantView(Trip selectedTrip)
        {
            InitializeComponent();
            _selectedTrip = selectedTrip;
            var dbContext = new TravelSystemDbContext();
            _viewModel = new RestaurantViewModel(dbContext, selectedTrip);

            DataContext = _viewModel;
        }

        private void AccommodationsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var w = new AccomodationView(_selectedTrip);
            w.Show();
            this.Close();
        }

        private void AttractionsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var w = new TouristAttractionView(_selectedTrip);
            w.Show();
            this.Close();
        }

        private void BackButtonClicked(Object sender, RoutedEventArgs e) {
            var w = new TripView();
            w.Show();
            this.Close();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var restaurant = (sender as Button).DataContext as Restaurant; 
            if (restaurant == null) return;

            var msg = $"Are you sure you want to delete the restaurant: {restaurant.Name}?";
            var result = MessageBox.Show(msg, "Confirm Delete", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _viewModel.DeleteRestaurantCommand.Execute(restaurant);
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var restaurant = (sender as Button).DataContext as Restaurant;
            if (restaurant == null) return;
            var dialog = new EditRestaurantDialogView(restaurant, _viewModel);
            dialog.Owner = this;
            dialog.ShowDialog();

        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddRestaurantDialogView(_viewModel);
            dialog.Owner = this;
            dialog.ShowDialog();

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

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            if (selectedItem != null && _viewModel != null)
            {
                string selectedContent = selectedItem.Content.ToString();
                if (selectedContent == "Italian Cuisine")
                {
                    List<Restaurant> filteredRes = FilterItalianCuisine();
                    UpdateCollection(filteredRes);
                }
                else if (selectedContent == "Mexican Cuisine")
                {
                    List<Restaurant> filteredRes = FilterMexicanCuisine();
                    UpdateCollection(filteredRes);
                }
                else if (selectedContent == "Serbian Cuisine")
                {
                    List<Restaurant> filteredRes = FilterSerbianCuisine();
                    UpdateCollection(filteredRes);
                }
                else if (selectedContent == "Chinese Cuisine")
                {
                    List<Restaurant> filteredRes = FilterChineseCuisine();
                    UpdateCollection(filteredRes);
                }
                else if (selectedContent == "No Filter")
                {
                    _viewModel.LoadRestaurants();
                }
            }


        }
        private void UpdateCollection(List<Restaurant> newRestaurants)
        {
            _viewModel.Restaurants.Clear();
            foreach (var Restaurant in newRestaurants)
            {
                _viewModel.Restaurants.Add(Restaurant);
            }
        }

        private List<Restaurant> FilterItalianCuisine()
        {
            _viewModel.LoadRestaurants();
            var Restaurants = _viewModel.Restaurants;
            return Restaurants.Where(Restaurant => Restaurant.Cuisine == "Italian").ToList();
        }

        private List<Restaurant> FilterChineseCuisine()
        {
            _viewModel.LoadRestaurants();
            var Restaurants = _viewModel.Restaurants;
            return Restaurants.Where(Restaurant => Restaurant.Cuisine == "Chinese").ToList();
        }

        private List<Restaurant> FilterSerbianCuisine()
        {
            _viewModel.LoadRestaurants();
            var Restaurants = _viewModel.Restaurants;
            return Restaurants.Where(Restaurant => Restaurant.Cuisine == "Serbian").ToList();
        }


        private List<Restaurant> FilterMexicanCuisine()
        {
            _viewModel.LoadRestaurants();
            var Restaurants = _viewModel.Restaurants;
            return Restaurants.Where(Restaurant => Restaurant.Cuisine == "Mexican").ToList();
        }



    }




}
