using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SoloTravelAgent.Navigation;
using SoloTravelAgent.View.MainScreen;
using SoloTravelAgent.ViewModel.BookingManagement;
using SoloTravelAgent.ViewModel.Report;
using System.Windows;

namespace SoloTravelAgent.ViewModel.MainScreen
{
    public class AgentMenuViewModel : ViewModelBase
    {
        private int _selectedOption;

        public int SelectedOption
        {
            get => _selectedOption;
            set => Set(ref _selectedOption, value);
        }

        public RelayCommand<string> RadioButtonCommand { get; private set; }

        public AgentMenuViewModel()
        {
            RadioButtonCommand = new RelayCommand<string>(RadioButtonChecked);
            SelectedOption = 1;
        }

        private void RadioButtonChecked(string selectedOption)
        {
            int option = int.Parse(selectedOption);
            SelectedOption = option;
            switch (SelectedOption)
            {
                case 1:
                    NavigationService.Instance.NavigateTo(new TripViewModel());
                    break;
                case 2:
                    NavigationService.Instance.NavigateTo(new TripSellReportViewModel());
                    break;
                case 3:
                    NavigationService.Instance.NavigateTo(new AgentBookingManagementViewModel());
                    break;
                case 5:
                    var w = new MainWindow();
                    w.Show();
                    Messenger.Default.Send(new NotificationMessage("CloseWindow"));
                    break;
            }
        }
    }
}
