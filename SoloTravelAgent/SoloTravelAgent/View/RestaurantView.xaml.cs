using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Navigation;
using SoloTravelAgent.View.DialogView;
using SoloTravelAgent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SoloTravelAgent.View
{
    /// <summary>
    /// Interaction logic for RestaurantView.xaml
    /// </summary>
    public partial class RestaurantView : UserControl
    {
        private RestaurantViewModel _viewModel;
        public Trip _selectedTrip;
        public RestaurantView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as RestaurantViewModel;
            _selectedTrip = _viewModel.SelectedTrip;
        }

        private void AccommodationsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var vm = new AccommodationsViewModel(_selectedTrip);
            NavigationService.Instance.NavigateTo(vm);
        }

        private void AttractionsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var vm = new TouristAttractionsViewModel(_selectedTrip);
            NavigationService.Instance.NavigateTo(vm);
        }

        private void BackButtonClicked(Object sender, RoutedEventArgs e)
        {
            var vm = new TripViewModel();
            NavigationService.Instance.NavigateTo(vm);
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
            dialog.Owner = Window.GetWindow(this);
            dialog.ShowDialog();
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddRestaurantDialogView(_viewModel);
            dialog.Owner = Window.GetWindow(this);
            dialog.ShowDialog();
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                var window = Window.GetWindow(this);

                if (IsMaximize)
                {
                    window.WindowState = WindowState.Normal;
                    window.Width = 1080;
                    window.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    window.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Window.GetWindow(this).DragMove();
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
