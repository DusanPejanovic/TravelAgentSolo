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
    /// Interaction logic for AddAccommodationDialogView.xaml
    /// </summary>
    public partial class AddAccommodationDialogView : Window
    {

        private readonly AddAccommodationViewModel _viewModel;

        public AddAccommodationDialogView(AccommodationsViewModel rvm)
        {
            InitializeComponent();
            _viewModel = new AddAccommodationViewModel();
            _viewModel.RequestClose += () => this.Close();
            _viewModel.SaveCommand = new RelayCommand(_ =>
            {
                var newAccommodation = new Accommodation
                {
                    Name = _viewModel.Name,
                    Address = _viewModel.Address,
                    Stars = _viewModel.Stars,
                    Website = _viewModel.Website,
                    PhoneNumber = _viewModel.PhoneNumber
                };
                rvm.AddAccommodation(newAccommodation);
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
