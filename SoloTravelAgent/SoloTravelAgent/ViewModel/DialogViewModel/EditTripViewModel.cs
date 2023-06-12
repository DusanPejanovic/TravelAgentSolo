using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SoloTravelAgent.Model.Entities;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight;

namespace SoloTravelAgent.ViewModel.DialogViewModel
{
    internal class EditTripViewModel : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Trip TripToEdit { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        private Action _updateTripAction;
        public event Action RequestClose = delegate { };

        public String originalName;
        public String originalDescription;
        public DateTime originalStartDate;
        public DateTime originalEndDate;
        public decimal originalPrice;
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
                    case nameof(Description):
                        return ValidateDescription();
                    case nameof(Price):
                        return ValidatePrice();

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
        private string ValidateDescription()
        {

            if (string.IsNullOrWhiteSpace(Description))
            {
                return "Description cannot be empty";
            }

            return null;
        }

        private string ValidatePrice()
        {

            if (Price < 0)
            {
                return "Wrong value";
            }

            return null;
        }
        public EditTripViewModel(Trip tripToEdit, Action updateTripAction)
        {
            originalName = tripToEdit.Name;
            originalDescription = tripToEdit.Description;
            originalStartDate = tripToEdit.StartDate;
            originalEndDate = tripToEdit.EndDate;
            originalPrice = tripToEdit.Price;

            TripToEdit = tripToEdit;
            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());
            _updateTripAction = updateTripAction;
        }

        public bool HasUnsavedChanges()
        {
            bool same = originalName == TripToEdit.Name &&
                        originalDescription == TripToEdit.Description &&
                        originalStartDate == TripToEdit.StartDate &&
                        originalEndDate == TripToEdit.EndDate &&
                        originalPrice == TripToEdit.Price;

            return !same;
        }


        public string Name
        {
            get => TripToEdit.Name;
            set
            {
                if (TripToEdit.Name != value)
                {
                    TripToEdit.Name = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Name));
                    RaisePropertyChanged();
                }
            }
        }

        public string Description
        {
            get => TripToEdit.Description;
            set
            {
                if (TripToEdit.Description != value)
                {
                    TripToEdit.Description = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Description));
                    RaisePropertyChanged();
                }
            }
        }

        public DateTime StartDate
        {
            get => TripToEdit.StartDate;
            set
            {
                if (TripToEdit.StartDate != value)
                {
                    TripToEdit.StartDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime EndDate
        {
            get => TripToEdit.EndDate;
            set
            {
                if (TripToEdit.EndDate != value)
                {
                    TripToEdit.EndDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal Price
        {
            get => TripToEdit.Price;
            set
            {
                if (TripToEdit.Price != value)
                {
                    TripToEdit.Price = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Price));
                    RaisePropertyChanged();
                }
            }
        }

        private void Save()
        {
            _updateTripAction?.Invoke();
            RequestClose.Invoke();
        }

        private bool CanSave()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description) || Price < 0)
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
