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
using SoloTravelAgent.ViewModel;
using SoloTravelAgent.ViewModel.DialogViewModel;

namespace SoloTravelAgent.View.DialogView
{
    /// <summary>
    /// Interaction logic for EditTripView.xaml
    /// </summary>
    public partial class EditTripView : Window
    {
        private Trip _selectedTrip;
        private readonly EditTripViewModel _viewModel;
        public EditTripView(Trip selectedTrip, TripViewModel rvm)
        {
            InitializeComponent();
            _selectedTrip = selectedTrip;
            var dbContext = new TravelSystemDbContext();
            _viewModel = new EditTripViewModel(selectedTrip, () => rvm.UpdateTrip());
            _viewModel.RequestClose += () => this.Close();


            DataContext = _viewModel;
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.HasUnsavedChanges())
            {
                var msg = $"You have unsaved changes. Are you sure you want to discard them?";
                var result = MessageBox.Show(msg, "Unsaved changes", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

    }
}
