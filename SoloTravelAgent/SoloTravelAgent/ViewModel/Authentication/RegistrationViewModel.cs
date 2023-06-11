using GalaSoft.MvvmLight;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Service;
using SoloTravelAgent.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace SoloTravelAgent.ViewModel.Authentication
{
    public class RegistrationViewModel : ViewModelBase, IDataErrorInfo
    {
        private MainViewModel mainViewModel;
        private AuthService _authService;
        public ICommand SwitchToLoginCommand { get; }
        public ICommand SignUpCommand { get; set; }

        private string _name;
        private string _email;
        private string _phoneNumber;
        private string _password = "";
        private string _repeatPassword = "";
        private Dictionary<string, bool> _dirtyProperties = new Dictionary<string, bool>();

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                MarkAsDirty(nameof(Name));
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(CanSubmitForm));
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                MarkAsDirty(nameof(Email));
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(CanSubmitForm));
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                MarkAsDirty(nameof(PhoneNumber));
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(CanSubmitForm));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                MarkAsDirty(nameof(Password));
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(RepeatPassword));
                RaisePropertyChanged(nameof(CanSubmitForm));
            }
        }

        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set
            {
                _repeatPassword = value;
                MarkAsDirty(nameof(RepeatPassword));
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(CanSubmitForm));
            }
        }

        public RegistrationViewModel(MainViewModel mainViewModel, TravelSystemDbContext dbContext)
        {
            _authService = new AuthService(dbContext);

            this.mainViewModel = mainViewModel;
            SwitchToLoginCommand = new RelayCommand(_ =>
            {
                SwitchToLogin();
            });

            SignUpCommand = new RelayCommand(
                _ => SignUp(),
                _ => CanSubmitForm
            );

            _dirtyProperties = new Dictionary<string, bool>
            {
                { nameof(Name), false },
                { nameof(Email), false },
                { nameof(PhoneNumber), false },
                { nameof(Password), false },
                { nameof(RepeatPassword), false },
            };
        }


        private void MarkAsDirty(string propertyName)
        {
            _dirtyProperties[propertyName] = true;
        }

        private bool IsPropertyDirty(string propertyName)
        {
            return _dirtyProperties.ContainsKey(propertyName) && _dirtyProperties[propertyName];
        }

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                if (!IsPropertyDirty(columnName))
                {
                    return null;
                }

                var emptyValidationResult = ValidateIfEmpty(columnName);
                if (!string.IsNullOrEmpty(emptyValidationResult))
                {
                    return emptyValidationResult;
                }

                switch (columnName)
                {
                    case nameof(Email):
                        if (Error != null)
                        {
                            return Error;
                        }
                        return ValidateEmail();
                    case nameof(PhoneNumber):
                        return ValidatePhoneNumber();
                    case nameof(RepeatPassword):
                        return ValidateRepeatPassword();
                    default:
                        return null;
                }
            }
        }

        private string ValidateIfEmpty(string propertyName)
        {
            string value = GetType().GetProperty(propertyName)?.GetValue(this, null) as string;

            if (string.IsNullOrEmpty(value))
            {
                return "This cannot be empty!";
            }

            return null;
        }

        private string ValidateEmail()
        {
            Regex regex = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
            if (!regex.IsMatch(_email))
            {
                return "Email is not in correct format!";
            }

            return null;
        }

        private string ValidatePhoneNumber()
        {
            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(_phoneNumber))
            {
                return "Phone number must contain only numbers!";
            }

            return null;
        }

        private string ValidateRepeatPassword()
        {
            if (!string.Equals(_password, _repeatPassword))
            {
                return "Passwords do not match!";
            }

            return null;
        }

        public bool CanSubmitForm
        {
            get
            {
                foreach (var property in _dirtyProperties)
                {
                    var columnName = property.Key;
                    var isDirty = property.Value;

                    if (!isDirty)
                    {
                        return false;
                    }

                    var validationMessage = this[columnName];
                    if (!string.IsNullOrEmpty(validationMessage))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private void SignUp()
        {
            if (_authService.IsEmailTaken(_email))
            {
                Error = "Email is already taken!";
                RaisePropertyChanged(nameof(Email));
                Error = null;
            }
            else
            {
                _authService.Register(_name, _email, _phoneNumber, _password);
                var w = new TripMarketView();
                w.Show();
            }
        }

        private void SwitchToLogin()
        {
            mainViewModel.ShowLoginView();
        }
    }
}
