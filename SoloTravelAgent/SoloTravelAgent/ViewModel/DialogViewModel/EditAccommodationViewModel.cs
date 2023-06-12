using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using SoloTravelAgent.Model.Entities;
using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace SoloTravelAgent.ViewModel.DialogViewModel
{
    internal class EditAccommodationViewModel : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Accommodation AccommodationToEdit { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        private Action _updateAccommodationAction;
        public event Action RequestClose = delegate { };

        public String originalName;
        public String originalAddress;
        public int originalStars;
        public String originalWebsite;
        public String originalPhoneNumber;

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
        public EditAccommodationViewModel(Accommodation accommodationToEdit, Action updateAccommodationAction)
        {
            originalName = accommodationToEdit.Name;
            originalAddress = accommodationToEdit.Address;
            originalStars = accommodationToEdit.Stars;
            originalWebsite = accommodationToEdit.Website;
            originalPhoneNumber = accommodationToEdit.PhoneNumber;

            AccommodationToEdit = accommodationToEdit;

            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());
            _updateAccommodationAction = updateAccommodationAction;
        }

        public bool HasUnsavedChanges()
        {
            bool same = originalName == AccommodationToEdit.Name &&
                        originalAddress == AccommodationToEdit.Address &&
                        originalStars == AccommodationToEdit.Stars &&
                        originalWebsite == AccommodationToEdit.Website &&
                        originalPhoneNumber == AccommodationToEdit.PhoneNumber;

            return !same;
        }

        public string Name
        {
            get => AccommodationToEdit.Name;
            set
            {
                if (AccommodationToEdit.Name != value)
                {
                    AccommodationToEdit.Name = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Name));
                    RaisePropertyChanged();
                }
            }
        }

        public string Address
        {
            get => AccommodationToEdit.Address;
            set
            {
                if (AccommodationToEdit.Address != value)
                {
                    AccommodationToEdit.Address = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Address));
                    RaisePropertyChanged();
                }
            }
        }

        public int Stars
        {
            get => AccommodationToEdit.Stars;
            set
            {
                if (AccommodationToEdit.Stars != value)
                {
                    AccommodationToEdit.Stars = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Stars));
                    RaisePropertyChanged();
                }
            }
        }

        public string Website
        {
            get => AccommodationToEdit.Website;
            set
            {
                if (AccommodationToEdit.Website != value)
                {
                    AccommodationToEdit.Website = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Website));
                    RaisePropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get => AccommodationToEdit.PhoneNumber;
            set
            {
                if (AccommodationToEdit.PhoneNumber != value)
                {
                    AccommodationToEdit.PhoneNumber = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(PhoneNumber));
                    RaisePropertyChanged();
                }
            }
        }

        private void Save()
        {
            _updateAccommodationAction?.Invoke();
            RequestClose.Invoke();
        }

        private bool CanSave()
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

