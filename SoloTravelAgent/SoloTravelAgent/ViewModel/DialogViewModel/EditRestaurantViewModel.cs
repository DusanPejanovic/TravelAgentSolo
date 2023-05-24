using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;

using SoloTravelAgent.Model.Entities;

namespace SoloTravelAgent.ViewModel.DialogViewModel
{
    internal class EditRestaurantViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Restaurant RestaurantToEdit { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }


        private Action _updateRestaurantAction;
        public event Action RequestClose = delegate { };

        public String originalName;
        public String originalAddress;
        public String originalCuisine;
        public String originalWebSite;
        public String originalPhoneNumber;

        public EditRestaurantViewModel(Restaurant restaurantToEdit, Action updateRestaurantAction)
        {
            originalName = restaurantToEdit.Name;
            originalAddress = restaurantToEdit.Address;
            originalCuisine = restaurantToEdit.Cuisine;
            originalPhoneNumber = restaurantToEdit.PhoneNumber;
            originalWebSite = restaurantToEdit.Website;
            RestaurantToEdit = restaurantToEdit;
            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());
            _updateRestaurantAction = updateRestaurantAction;
        }

        public bool HasUnsavedChanges()
        {
            bool same = originalName == RestaurantToEdit.Name &&
                        originalPhoneNumber == RestaurantToEdit.PhoneNumber &&
                        originalWebSite == RestaurantToEdit.Website &&
                        originalAddress == RestaurantToEdit.Address &&
                        originalCuisine == RestaurantToEdit.Cuisine;

            return !same;
        }


        public string Name
        {
            get => RestaurantToEdit.Name;
            set
            {
                if (RestaurantToEdit.Name != value)
                {
                    RestaurantToEdit.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Address
        {
            get => RestaurantToEdit.Address;
            set
            {
                if (RestaurantToEdit.Address != value)
                {
                    RestaurantToEdit.Address = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Cuisine
        {
            get => RestaurantToEdit.Cuisine;
            set
            {
                if (RestaurantToEdit.Cuisine != value)
                {
                    RestaurantToEdit.Cuisine = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Website
        {
            get => RestaurantToEdit.Website;
            set
            {
                if (RestaurantToEdit.Website != value)
                {
                    RestaurantToEdit.Website = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get => RestaurantToEdit.PhoneNumber;
            set
            {
                if (RestaurantToEdit.PhoneNumber != value)
                {
                    RestaurantToEdit.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }


        private void Save()
        {
            _updateRestaurantAction?.Invoke();
            RequestClose.Invoke();
        }

        private bool CanSave()
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
