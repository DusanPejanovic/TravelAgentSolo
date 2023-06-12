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
    /// Interaction logic for TouristAttractionMarketView.xaml
    /// </summary>
    public partial class TouristAttractionMarketView : UserControl
    {
        private TouristAttractionsViewModel _viewModel;
        private Trip _selectedTrip;

        public TouristAttractionMarketView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as TouristAttractionsViewModel;
            _selectedTrip = _viewModel.SelectedTrip;
        }

        private void BackButtonClicked(Object sender, RoutedEventArgs e)
        {
            var vm = new TripViewModel();
            NavigationService.Instance.NavigateTo(vm);
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

        private void AccommodationsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var vm = new AccommodationsViewModel(_selectedTrip);
            NavigationService.Instance.NavigateTo(vm);
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var attraction = (sender as Button).DataContext as TouristAttraction;
            if (attraction == null) return;
            var dialog = new EditAttractionDialogView(attraction, _viewModel);
            dialog.Owner = Window.GetWindow(this); // ovo mi je roditelj prozor
            dialog.ShowDialog();

        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddAttractionDialogView(_viewModel);
            dialog.Owner = Window.GetWindow(this);
            dialog.ShowDialog();

        }

        private void RestaurantRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var vm = new RestaurantViewModel(_selectedTrip);
            NavigationService.Instance.NavigateTo(vm);
        }
        private void DescriptionRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var vm = new DescriptionViewModel(_selectedTrip);
            NavigationService.Instance.NavigateTo(vm);
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
