using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.View.DialogView;
using SoloTravelAgent.ViewModel;

namespace SoloTravelAgent.View
{
    /// <summary>
    /// Interaction logic for TouristAttractionView.xaml
    /// </summary>
    public partial class TouristAttractionView : Window
    {

        private readonly TouristAttractionsViewModel _viewModel;
        private Trip _selectedTrip;

        public TouristAttractionView(Trip selectedTrip)
        {
            InitializeComponent();
            var dbContext = new TravelSystemDbContext();
            _viewModel = new TouristAttractionsViewModel(dbContext, selectedTrip);
            _selectedTrip = selectedTrip;

            DataContext = _viewModel;

        }

        private void BackButtonClicked(Object sender, RoutedEventArgs e)
        {
            //var w = new TripView();
            //w.Show();
            //this.Close();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var attraction = (sender as Button).DataContext as TouristAttraction;
            if (attraction == null) return;

            var msg = $"Are you sure you want to delete the tourist attraction: {attraction.Name}?";
            var result = MessageBox.Show(msg, "Confirm Delete", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _viewModel.DeleteTouristAttractionCommand.Execute(attraction);
            }
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

        private void AccommodationsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var accommodationView = new AccomodationView(_selectedTrip);
            accommodationView.Show();
            this.Close();
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var attraction = (sender as Button).DataContext as TouristAttraction;
            if (attraction == null) return;
            var dialog = new EditAttractionDialogView(attraction, _viewModel);
            dialog.Owner = this; // ovo mi je roditelj prozor
            dialog.ShowDialog();

        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddAttractionDialogView(_viewModel);
            dialog.Owner = this;
            dialog.ShowDialog();

        }


        private void RestaurantRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var w = new RestaurantView(_selectedTrip);
            w.Show();
            this.Close();
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

                if (selectedContent == "Fee >= 1500")
                {
                    List<TouristAttraction> filteredAttractons = FilterPriceGreaterThanOrEqualTo1500();
                    UpdateTouristCollection(filteredAttractons);
                }
                else if (selectedContent == "Fee < 1500")
                {
                    List<TouristAttraction> filteredAttr = FilterPriceLowerThan1500();
                    UpdateTouristCollection(filteredAttr);
                }
                else if (selectedContent == "No Filter")
                {
                    _viewModel.LoadTouristAttractions();
                }

            }
        }

        private List<TouristAttraction> FilterPriceGreaterThanOrEqualTo1500()
        {
            _viewModel.LoadTouristAttractions();
            var attractions = _viewModel.TouristAttractions;
            return attractions.Where(attraction => attraction.EntryFee >= 1500).ToList();
        }

        private List<TouristAttraction> FilterPriceLowerThan1500()
        {
            _viewModel.LoadTouristAttractions();
            var attractions = _viewModel.TouristAttractions;
            return attractions.Where(attraction => attraction.EntryFee < 1500).ToList();
        }

        private void UpdateTouristCollection(List<TouristAttraction> newAttractions)
        {
            _viewModel.TouristAttractions.Clear();
            foreach (var attraction in newAttractions)
            {
                _viewModel.TouristAttractions.Add(attraction);
            }
        }


       
    }


}
