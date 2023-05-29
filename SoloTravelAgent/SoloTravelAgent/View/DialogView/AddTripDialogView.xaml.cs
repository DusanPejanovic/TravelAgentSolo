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
using SoloTravelAgent.View.DragDrop;
using SoloTravelAgent.ViewModel;
using SoloTravelAgent.ViewModels.DialogViewModels;

namespace SoloTravelAgent.View.DialogView
{
    /// <summary>
    /// Interaction logic for AddTripDialogView.xaml
    /// </summary>
    public partial class AddTripDialogView : Window
    {
        private readonly AddTripDialogViewModel _viewModel;
        public AddTripDialogView(TripViewModel tvm)
        {
            InitializeComponent();
            var dbContext = new TravelSystemDbContext();
            _viewModel = new AddTripDialogViewModel(restaurantListBox, accommodationsListBox, attractionsListBox);
            DataContext = _viewModel;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DragDrop_Click(object sender, RoutedEventArgs e) {

            var w = new AttractionsDragDrop(_viewModel);
            w.Show();
        }
}
}
