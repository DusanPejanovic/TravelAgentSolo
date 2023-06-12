using GalaSoft.MvvmLight;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Navigation;

namespace SoloTravelAgent.ViewModel.MainScreen
{
    public class ClientMainScreenViewModel: ViewModelBase
    {
        public ClientMenuViewModel MenuControlVM { get; set; }

        private object _currentViewModel;

        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                RaisePropertyChanged();
            }
        }

        public ClientMainScreenViewModel()
        {
            CurrentViewModel = new TripMarketViewModel();
            MenuControlVM = new ClientMenuViewModel();

            NavigationService.Instance.Navigated += OnNavigated;
        }

        private void OnNavigated()
        { 
            CurrentViewModel = NavigationService.Instance.CurrentViewModel;
            RaisePropertyChanged(nameof(CurrentViewModel));
        }
    }
}
