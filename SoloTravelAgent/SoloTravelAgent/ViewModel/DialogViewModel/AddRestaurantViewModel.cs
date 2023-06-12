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
using GalaSoft.MvvmLight;

namespace SoloTravelAgent.ViewModel.DialogViewModel
{
    internal class AddRestaurantViewModel : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Restaurant NewRestaurant { get; private set; }

        public ICommand SaveCommand { get;  set; }

        public ICommand CancelCommand { get; private set; }

        private Action _addRestaurantAction;
        public event Action RequestClose = delegate { };
        private Dictionary<string, bool> _dirtyProperties = new Dictionary<string, bool>();

        public AddRestaurantViewModel()
        {
            NewRestaurant = new Restaurant();
            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());
         
        }

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


        public string Name
        {
            get => NewRestaurant.Name;
            set
            {
                if (NewRestaurant.Name != value)
                {
                    NewRestaurant.Name = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Name));
                    RaisePropertyChanged();
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
                    MarkAsDirty(nameof(Address));
                    RaisePropertyChanged();
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
                    MarkAsDirty(nameof(Cuisine));
                    RaisePropertyChanged();
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
                    MarkAsDirty(nameof(Website));
                    RaisePropertyChanged();
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
                    MarkAsDirty(nameof(PhoneNumber));
                    RaisePropertyChanged();
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
