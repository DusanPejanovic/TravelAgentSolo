using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SoloTravelAgent.Model.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SoloTravelAgent.ViewModel.DialogViewModel
{
    internal class AddRestaurantViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Restaurant NewRestaurant { get; private set; }

        public ICommand SaveCommand { get;  set; }

        public ICommand CancelCommand { get; private set; }

        private Action _addRestaurantAction;
        public event Action RequestClose = delegate { };

        public AddRestaurantViewModel()
        {
            NewRestaurant = new Restaurant();
            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());
         
        }

        public string Name
        {
            get => NewRestaurant.Name;
            set
            {
                if (NewRestaurant.Name != value)
                {
                    NewRestaurant.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Address
        {
            get => NewRestaurant.Address;
            set
            {
                if (NewRestaurant.Address != value)
                {
                    NewRestaurant.Address = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Cuisine
        {
            get => NewRestaurant.Cuisine;
            set
            {
                if (NewRestaurant.Cuisine != value)
                {
                    NewRestaurant.Cuisine = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Website
        {
            get => NewRestaurant.Website;
            set
            {
                if (NewRestaurant.Website != value)
                {
                    NewRestaurant.Website = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get => NewRestaurant.PhoneNumber;
            set
            {
                if (NewRestaurant.PhoneNumber != value)
                {
                    NewRestaurant.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Save()
        {
            _addRestaurantAction?.Invoke();
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
