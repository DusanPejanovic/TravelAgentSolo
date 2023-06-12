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
    internal class EditAttractionViewModel : ViewModelBase, INotifyPropertyChanged, IDataErrorInfo { 
        public event PropertyChangedEventHandler PropertyChanged;

        public TouristAttraction AttractionToEdit { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        private Action _updateAttractionAction;
        public event Action RequestClose = delegate { };

        public String originalName;
        public String originalAddress;
        public String originalDescription;
        public decimal originalEntryFee;
        public String originalWebsite;
        private Dictionary<string, bool> _dirtyProperties = new Dictionary<string, bool>();
        public EditAttractionViewModel(TouristAttraction attractionToEdit, Action updateAttractionAction)
        {
            originalName = attractionToEdit.Name;
            originalAddress = attractionToEdit.Address;
            originalDescription = attractionToEdit.Description;
            originalEntryFee = attractionToEdit.EntryFee;
            originalWebsite = attractionToEdit.Website;

            AttractionToEdit = attractionToEdit;

            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());
            _updateAttractionAction = updateAttractionAction;
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

        public bool HasUnsavedChanges()
        {
            bool same = originalName == AttractionToEdit.Name &&
                        originalAddress == AttractionToEdit.Address &&
                        originalDescription == AttractionToEdit.Description &&
                        originalEntryFee == AttractionToEdit.EntryFee &&
                        originalWebsite == AttractionToEdit.Website;

            return !same;
        }

        public string Name
        {
            get => AttractionToEdit.Name;
            set
            {
                if (AttractionToEdit.Name != value)
                {
                    AttractionToEdit.Name = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Name));
                    RaisePropertyChanged();
                }
            }
        }

        public string Address
        {
            get => AttractionToEdit.Address;
            set
            {
                if (AttractionToEdit.Address != value)
                {
                    AttractionToEdit.Address = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Address));
                    RaisePropertyChanged();
                }
            }
        }

        public string Description
        {
            get => AttractionToEdit.Description;
            set
            {
                if (AttractionToEdit.Description != value)
                {
                    AttractionToEdit.Description = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Description));
                    RaisePropertyChanged();
                }
            }
        }

        public decimal EntryFee
        {
            get => AttractionToEdit.EntryFee;
            set
            {
                if (AttractionToEdit.EntryFee != value)
                {
                    AttractionToEdit.EntryFee = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(EntryFee));
                    RaisePropertyChanged();
                }
            }
        }

        public string Website
        {
            get => AttractionToEdit.Website;
            set
            {
                if (AttractionToEdit.Website != value)
                {
                    AttractionToEdit.Website = value;
                    OnPropertyChanged();
                    MarkAsDirty(nameof(Website));
                    RaisePropertyChanged();
                }
            }
        }

        private void Save()
        {
            _updateAttractionAction?.Invoke();
            RequestClose.Invoke();
        }

        private bool CanSave()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Address) || EntryFee < 0 || string.IsNullOrEmpty(Website) || string.IsNullOrEmpty(Description))
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
