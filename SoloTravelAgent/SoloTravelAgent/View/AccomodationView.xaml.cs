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
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.View.DialogView;
using SoloTravelAgent.ViewModel;

namespace SoloTravelAgent.View
{
    /// <summary>
    /// Interaction logic for AccomodationView.xaml
    /// </summary>
    public partial class AccomodationView : Window
    {
        private readonly AccommodationsViewModel _viewModel;
        private Trip _selectedTrip;
        public AccomodationView(Trip selectedTrip)
        {
            InitializeComponent();
            _selectedTrip = selectedTrip;
            var dbContext = new TravelSystemDbContext();
            _viewModel = new AccommodationsViewModel(dbContext, selectedTrip);
            
          
            DataContext = _viewModel;

        }
        private void BackButtonClicked(Object sender, RoutedEventArgs e)
        {
            var w = new TripView();
            w.Show();
            this.Close();
        }

        public int RestaurantCount
        {
            get => _selectedTrip?.Restaurants.Count ?? 0;
        }

        private void RestaurantRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var w = new RestaurantView(_selectedTrip);
            w.Show();
            this.Close();
        }

        private void AttractionsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var w = new TouristAttractionView(_selectedTrip);
            w.Show();
            this.Close();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var accommodation = (sender as Button).DataContext as Accommodation;
            if (accommodation == null) return;

            var msg = $"Are you sure you want to delete the accommodation: {accommodation.Name}?";
            var result = MessageBox.Show(msg, "Confirm Delete", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _viewModel.DeleteAccommodationCommand.Execute(accommodation);
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var accommodation = (sender as Button).DataContext as Accommodation;
            if (accommodation == null) return;
            var dialog = new EditAccommodationDialogView(accommodation, _viewModel);
            dialog.Owner = this;
            dialog.ShowDialog();

        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddAccommodationDialogView(_viewModel);
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

                if (selectedContent == "Stars == 5")
                {
                    List<Accommodation> filteredAcc = FilterBestAccommodation();
                    UpdateCollection(filteredAcc);
                }
                else if (selectedContent == "Stars >= 3")
                {
                    List<Accommodation> filteredAcc = FilteBetterThan3();
                    UpdateCollection(filteredAcc);
                }
                else if (selectedContent == "No Filter")
                {
                    _viewModel.LoadAccommodations();
                }


            }
        }

        private List<Accommodation> FilterBestAccommodation()
        {
            _viewModel.LoadAccommodations();
            var Accommodations = _viewModel.Accommodations;
            return Accommodations.Where(Accommodation => Accommodation.Stars == 5).ToList();
        }

        private List<Accommodation> FilteBetterThan3()
        {
            _viewModel.LoadAccommodations();
            var  Accommodations = _viewModel.Accommodations;
            return Accommodations.Where(Accommodation => Accommodation.Stars >= 3).ToList();
        }



        private void UpdateCollection(List<Accommodation> newAttractions)
        {
            _viewModel.Accommodations.Clear();
            foreach (var attraction in newAttractions)
            {
                _viewModel.Accommodations.Add(attraction);
            }
        }
    }
}
