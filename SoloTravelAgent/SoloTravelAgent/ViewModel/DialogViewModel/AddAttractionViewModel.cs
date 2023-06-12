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
    public class AddAttractionViewModel : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public TouristAttraction NewAttraction { get; private set; }

        public ICommand SaveCommand { get; set; }

        public ICommand CancelCommand { get; private set; }

        private Action _addAttractionAction;
        public event Action RequestClose = delegate { };
        private Dictionary<string, bool> _dirtyProperties = new Dictionary<string, bool>();

        public AddAttractionViewModel()
        {
            NewAttraction = new TouristAttraction();
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
                    case nameof(Description):
                        return ValidateDescription();
                    case nameof(EntryFee):
                        return ValidateEntryFee();
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

        private string ValidateEntryFee()
        {

            if (EntryFee < 0)
            {
                return "Wrong value";
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

        public string Name
        {
            get => NewAttraction.Name;
            set
            {
                if (NewAttraction.Name != value)
                {
                    NewAttraction.Name = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Name));
                    RaisePropertyChanged();
                }
            }
        }

        public string Address
        {
            get => NewAttraction.Address;
            set
            {
                if (NewAttraction.Address != value)
                {
                    NewAttraction.Address = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Address));
                    RaisePropertyChanged();
                }
            }
        }

        public decimal EntryFee
        {
            get => NewAttraction.EntryFee;
            set
            {
                if (NewAttraction.EntryFee != value)
                {
                    NewAttraction.EntryFee = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(EntryFee));
                    RaisePropertyChanged();
                }
            }
        }

        public string Website
        {
            get => NewAttraction.Website;
            set
            {
                if (NewAttraction.Website != value)
                {
                    NewAttraction.Website = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Website));
                    RaisePropertyChanged();
                }
            }
        }

        public string Description
        {
            get => NewAttraction.Description;
            set
            {
                if (NewAttraction.Description != value)
                {
                    NewAttraction.Description = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Description));
                    RaisePropertyChanged();
                }
            }
        }

        private void Save()
        {
            _addAttractionAction?.Invoke();
            RequestClose.Invoke();
        }

        public bool CanSave()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Address) || EntryFee < 0  || string.IsNullOrEmpty(Website) || string.IsNullOrEmpty(Description))
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
