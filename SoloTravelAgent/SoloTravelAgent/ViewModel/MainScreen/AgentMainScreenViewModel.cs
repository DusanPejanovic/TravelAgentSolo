using GalaSoft.MvvmLight;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Navigation;
using System.ComponentModel;

namespace SoloTravelAgent.ViewModel.MainScreen
{
    public class AgentMainScreenViewModel : ViewModelBase
    {
        public AgentMenuViewModel MenuControlVM { get; set; }

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

        public AgentMainScreenViewModel()
        {
            CurrentViewModel = new TripViewModel();
            MenuControlVM = new AgentMenuViewModel();

            NavigationService.Instance.Navigated += OnNavigated;
        }

        private void OnNavigated()
        {
            CurrentViewModel = NavigationService.Instance.CurrentViewModel;
            RaisePropertyChanged(nameof(CurrentViewModel));
        }
    }
}
