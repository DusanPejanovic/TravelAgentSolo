using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SoloTravelAgent.Navigation;

namespace SoloTravelAgent.ViewModel.MainScreen
{
    public class ClientMenuViewModel: ViewModelBase
    {
        private int _selectedOption;

        public int SelectedOption
        {
            get => _selectedOption;
            set => Set(ref _selectedOption, value);
        }

        public RelayCommand<string> RadioButtonCommand { get; private set; }

        public ClientMenuViewModel()
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
                    NavigationService.Instance.NavigateTo(new TripMarketViewModel());
                    break;
                case 2:
                    NavigationService.Instance.NavigateTo(new AgentBookingsViewModel());
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
