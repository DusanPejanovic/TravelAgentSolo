using SoloTravelAgent.Model.DTO;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.ViewModel.Report;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SoloTravelAgent.View.Report
{
    /// <summary>
    /// Interaction logic for TripSellReportView.xaml
    /// </summary>
    public partial class TripSellReportView : UserControl
    {
        public TripSellReportView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tripStatistics = (sender as Button).DataContext as TripStatistics;
            if (tripStatistics == null) return;
            var viewModel = DataContext as TripSellReportViewModel;
            viewModel.ShowSoldBookings(tripStatistics.TripId);

        }

        private void myDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (tripsDataGrid.SelectedItem != null)
            {
                var viewModel = DataContext as TripSellReportViewModel;
                var selectedItem = (TripStatistics)tripsDataGrid.SelectedItem;
                viewModel.ShowSoldBookings(selectedItem.TripId);
            }
        }
    }
}
