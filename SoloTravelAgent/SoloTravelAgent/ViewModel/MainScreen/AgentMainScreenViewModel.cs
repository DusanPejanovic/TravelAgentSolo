using GalaSoft.MvvmLight;
using SoloTravelAgent.Model.Data;
using System.ComponentModel;

namespace SoloTravelAgent.ViewModel.MainScreen
{
    public class AgentMainScreenViewModel : ViewModelBase
    {
        private TravelSystemDbContext _systemDbContext;
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

        public void ChangeViewModel(int option)
        {
            object vm = new object();
            switch (option)
            {
                case 1:
                    vm = new TripViewModel(_systemDbContext);
                    break;
                case 2:
                    vm = new AgentBookingsViewModel(_systemDbContext);
                    break;
            }

            CurrentViewModel = vm;
        }

        public AgentMainScreenViewModel()
        {
            _systemDbContext = new TravelSystemDbContext();
            CurrentViewModel = new TripViewModel(_systemDbContext);
            MenuControlVM = new AgentMenuViewModel();
            MenuControlVM.ParentViewModel = this;
        }
    }
}
