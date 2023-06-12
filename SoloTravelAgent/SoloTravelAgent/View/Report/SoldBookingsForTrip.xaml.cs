using SoloTravelAgent.Navigation;
using SoloTravelAgent.ViewModel.Report;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SoloTravelAgent.View.Report
{
    /// <summary>
    /// Interaction logic for SoldBookingsForTrip.xaml
    /// </summary>
    public partial class SoldBookingsForTrip : UserControl
    {
        public SoldBookingsForTrip()
        {
            InitializeComponent();
        }

        private void BackButtonClicked(Object sender, RoutedEventArgs e)
        {
            var vm = new TripSellReportViewModel();
            NavigationService.Instance.NavigateTo(vm);
        }
    }
}
