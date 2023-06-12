﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace SoloTravelAgent.ViewModel.MainScreen
{
    public class ClientMenuViewModel: ViewModelBase
    {
        private ClientMainScreenViewModel _parentViewModel;
        public ClientMainScreenViewModel ParentViewModel
        {
            get { return _parentViewModel; }
            set
            {
                _parentViewModel = value;
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
            ParentViewModel.ChangeViewModel(option);
        }
    }
}
