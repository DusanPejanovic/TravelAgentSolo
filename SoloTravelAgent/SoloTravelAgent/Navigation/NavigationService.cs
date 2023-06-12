using System;

namespace SoloTravelAgent.Navigation
{
    public class NavigationService
    {
        private static NavigationService _instance;
        public static NavigationService Instance => _instance ??= new NavigationService();

        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnNavigated();
            }
        }

        public event Action Navigated;

        private void OnNavigated()
        {
            Navigated?.Invoke();
        }

        public void NavigateTo(object viewModel)
        {
            CurrentViewModel = viewModel;
        }
    }
}
