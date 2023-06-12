using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Navigation;
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
    /// Interaction logic for DescriptionView.xaml
    /// </summary>
    public partial class DescriptionView : UserControl
    {
        private DescriptionViewModel _viewModel;
        private Trip _selectedTrip;

        public DescriptionView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as DescriptionViewModel;
            _selectedTrip = _viewModel.SelectedTrip;
        }

        private void BackButtonClicked(Object sender, RoutedEventArgs e)
        {
            var vm = new TripViewModel();
            NavigationService.Instance.NavigateTo(vm);
        }

        public int RestaurantCount
        {
            get => _selectedTrip?.Restaurants.Count ?? 0;
        }

        private void RestaurantRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var vm = new RestaurantViewModel(_selectedTrip);
            NavigationService.Instance.NavigateTo(vm);
        }

        private void AttractionsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var vm = new TouristAttractionsViewModel(_selectedTrip);
            NavigationService.Instance.NavigateTo(vm);
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
            var Accommodations = _viewModel.Accommodations;
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
