﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Navigation;
using SoloTravelAgent.ViewModel.Report;

namespace SoloTravelAgent.ViewModel.MainScreen
{
    public class ClientMenuViewModel: ViewModelBase
    {
        public User CurrentUser
        {
            get
            {
                return AuthenticationManager.CurrentUser;
            }
            set
            {
                AuthenticationManager.CurrentUser = value;
                RaisePropertyChanged();
            }
        }

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
                    NavigationService.Instance.NavigateTo(new TripBookedViewModel());
                    break;
                case 3:
                    NavigationService.Instance.NavigateTo(new TripHistoryViewModel());
                    break;
                case 5:
                    Logout();
                    var w = new MainWindow();
                    w.Show();
                    Messenger.Default.Send(new NotificationMessage("CloseWindow"));
                    break;
            }
        }

        public void Logout()
        {
            AuthenticationManager.Logout();
            CurrentUser = null; 
        }
    }
}
