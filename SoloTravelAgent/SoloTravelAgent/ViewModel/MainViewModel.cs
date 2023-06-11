using GalaSoft.MvvmLight;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.ViewModel.Authentication;

namespace SoloTravelAgent.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private TravelSystemDbContext _systemDbContext;
        private ViewModelBase currentView;

        public ViewModelBase CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel(TravelSystemDbContext dbContext)
        {
            _systemDbContext = dbContext;
            CurrentView = new LoginViewModel(this, _systemDbContext);
        }

        public void ShowLoginView()
        {
            CurrentView = new LoginViewModel(this, _systemDbContext);
        }

        public void ShowRegistrationView()
        {
            CurrentView = new RegistrationViewModel(this, _systemDbContext);
        }
    }
}
