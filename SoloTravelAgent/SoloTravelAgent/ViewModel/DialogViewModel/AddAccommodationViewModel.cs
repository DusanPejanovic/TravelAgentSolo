using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SoloTravelAgent.Model.Entities;


namespace SoloTravelAgent.ViewModel.DialogViewModel
{
    internal class AddAccommodationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Accommodation NewAccommodation { get; set; }

        public ICommand SaveCommand { get; set; }

        public ICommand CancelCommand { get; private set; }

        private Action _addAccommodationAction;
        public event Action RequestClose = delegate { };

        public AddAccommodationViewModel()
        {
            NewAccommodation = new Accommodation();
            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());
        }


        public string Name
        {
            get => NewAccommodation.Name;
            set
            {
                if (NewAccommodation.Name != value)
                {
                    NewAccommodation.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Address
        {
            get => NewAccommodation.Address;
            set
            {
                if (NewAccommodation.Address != value)
                {
                    NewAccommodation.Address = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Stars
        {
            get => NewAccommodation.Stars;
            set
            {
                if (NewAccommodation.Stars != value)
                {
                    NewAccommodation.Stars = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Website
        {
            get => NewAccommodation.Website;
            set
            {
                if (NewAccommodation.Website != value)
                {
                    NewAccommodation.Website = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get => NewAccommodation.PhoneNumber;
            set
            {
                if (NewAccommodation.PhoneNumber != value)
                {
                    NewAccommodation.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }


        private void Save()
        {
            _addAccommodationAction?.Invoke();
            RequestClose.Invoke();
        }

        public bool CanSave()
        {
            // Add your logic to determine if the Save button should be enabled
            return true;
        }

        private void Cancel()
        {
            RequestClose.Invoke();
        }

        public void SetAddAccommodationAction(Action addAccommodationAction)
        {
            _addAccommodationAction = addAccommodationAction;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
