using GalaSoft.MvvmLight;
using SoloTravelAgent.Model.Data;

namespace SoloTravelAgent.ViewModel.MainScreen
{
    public class ClientMainScreenViewModel: ViewModelBase
    {
        private TravelSystemDbContext _systemDbContext;
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

        public void ChangeViewModel(int option)
        {
            object vm = new object();
            switch (option)
            {
                case 1:
                    vm = new TripMarketViewModel();
                    break;
                case 2:
                    vm = new AgentBookingsViewModel(_systemDbContext);
                    break;
            }

            CurrentViewModel = vm;
        }

        public ClientMainScreenViewModel()
        {
            _systemDbContext = new TravelSystemDbContext();
            CurrentViewModel = new TripMarketViewModel();
            MenuControlVM = new ClientMenuViewModel();
            MenuControlVM.ParentViewModel = this;
        }
    }
}
