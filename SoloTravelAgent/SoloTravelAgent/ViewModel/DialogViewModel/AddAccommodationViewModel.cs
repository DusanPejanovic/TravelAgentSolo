using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SoloTravelAgent.Model.Entities;
using GalaSoft.MvvmLight;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Service;
using SoloTravelAgent.View;
using System.Text.RegularExpressions;
using System.Windows;





namespace SoloTravelAgent.ViewModel.DialogViewModel
{
    internal class AddAccommodationViewModel : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Accommodation NewAccommodation { get; set; }

        public ICommand SaveCommand { get; set; }

        public ICommand CancelCommand { get; private set; }
        private Dictionary<string, bool> _dirtyProperties = new Dictionary<string, bool>();

        private Action _addAccommodationAction;
        public event Action RequestClose = delegate { };

        public AddAccommodationViewModel()
        {
            NewAccommodation = new Accommodation();
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
        public string Name
        {
            get => NewAccommodation.Name;
            set
            {
                if (NewAccommodation.Name != value)
                {
                    NewAccommodation.Name = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Name));
                    RaisePropertyChanged();
                }
            }
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
                    case nameof(PhoneNumber):
                        return ValidateContact();
                    case nameof(Stars):
                        return ValidateStars();
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

        private string ValidateStars()
        {

            if (Stars < 0 || Stars > 5)
            {
                return "Wrong value";
            }

            return null;
        }

        private string ValidateContact()
        {

            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                return "Contact cannot be empty";
            }

            return null;
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
                    MarkAsDirty(nameof(Address));
                    RaisePropertyChanged();
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
                    MarkAsDirty(nameof(Stars));
                    RaisePropertyChanged();
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
                    MarkAsDirty(nameof(Website));
                    RaisePropertyChanged();
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
                    MarkAsDirty(nameof(PhoneNumber));
                    RaisePropertyChanged();
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
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Address) || Stars <= 0 || Stars > 5 || string.IsNullOrEmpty(Website) || string.IsNullOrEmpty(PhoneNumber))
            {

                return false;
            }
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
