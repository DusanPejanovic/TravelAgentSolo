using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;

using SoloTravelAgent.Model.Entities;
using GalaSoft.MvvmLight;

namespace SoloTravelAgent.ViewModel.DialogViewModel
{
    internal class EditRestaurantViewModel: ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Restaurant RestaurantToEdit { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }
        private Dictionary<string, bool> _dirtyProperties = new Dictionary<string, bool>();


        public string Error { get; set; }
        private void MarkAsDirty(string propertyName)
        {
            _dirtyProperties[propertyName] = true;
        }

        private bool IsPropertyDirty(string propertyName)
        {
            return _dirtyProperties.ContainsKey(propertyName) && _dirtyProperties[propertyName];
        }

        public string this[string columnName]
        {
            get
            {
                if (!IsPropertyDirty(columnName))
                {
                    return null;
                }

                switch (columnName)
                {
                    case nameof(Name):
                        return ValidateName();
                    case nameof(Address):
                        return ValidateAddress();
                    case nameof(Cuisine):
                        return ValidateCuisine();
                    case nameof(PhoneNumber):
                        return ValidatePhoneNumber();
                    case nameof(Website):
                        return ValidateWebSite();

                    default:
                        return null;
                }
            }
        }

        private string ValidateName()
        {

            if (string.IsNullOrWhiteSpace(Name))
            {
                return "Name cannot be empty";
            }

            return null;
        }
        private string ValidateAddress()
        {

            if (string.IsNullOrWhiteSpace(Address))
            {
                return "Address cannot be empty";
            }

            return null;
        }

        private string ValidateWebSite()
        {

            if (string.IsNullOrWhiteSpace(Website))
            {
                return "Website cannot be empty";
            }

            return null;
        }

        private string ValidatePhoneNumber()
        {

            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                return "Wrong value";
            }

            return null;
        }

        private string ValidateCuisine()
        {

            if (string.IsNullOrWhiteSpace(Cuisine))
            {
                return "Cuisine cannot be empty";
            }

            return null;
        }
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
                    MarkAsDirty(nameof(Name));
                    RaisePropertyChanged();
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
                    MarkAsDirty(nameof(Address));
                    RaisePropertyChanged();
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
                    MarkAsDirty(nameof(Cuisine));
                    RaisePropertyChanged();
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
                    MarkAsDirty(nameof(Website));
                    RaisePropertyChanged();
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
                    MarkAsDirty(nameof(PhoneNumber));
                    RaisePropertyChanged();
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
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Cuisine) || string.IsNullOrEmpty(Website) || string.IsNullOrEmpty(PhoneNumber))
            {

                return false;
            }
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
