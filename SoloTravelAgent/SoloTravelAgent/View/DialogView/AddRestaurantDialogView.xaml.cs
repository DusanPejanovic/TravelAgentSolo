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
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.ViewModel;
using SoloTravelAgent.ViewModel.DialogViewModel;

namespace SoloTravelAgent.View.DialogView
{
    /// <summary>
    /// Interaction logic for AddRestaurantDialogView.xaml
    /// </summary>
    public partial class AddRestaurantDialogView : Window
    {
        private readonly AddRestaurantViewModel _viewModel;

        public AddRestaurantDialogView(RestaurantViewModel rvm)
        {
            InitializeComponent();
            _viewModel = new AddRestaurantViewModel();
            _viewModel.RequestClose += () => this.Close();
            _viewModel.SaveCommand = new RelayCommand(_ =>
            {
                var newRestaurant = new Restaurant
                {
                    Name = _viewModel.Name,
                    Address = _viewModel.Address,
                    Cuisine = _viewModel.Cuisine,
                    Website = _viewModel.Website,
                    PhoneNumber = _viewModel.PhoneNumber
                };
                rvm.AddRestaurant(newRestaurant);
                this.Close();
            }, _ => _viewModel.CanSave());
            DataContext = _viewModel;
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }
    }

}
