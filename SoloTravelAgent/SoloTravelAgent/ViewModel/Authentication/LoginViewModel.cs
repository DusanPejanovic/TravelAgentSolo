using GalaSoft.MvvmLight;
using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;
using SoloTravelAgent.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace SoloTravelAgent.ViewModel.Authentication
{
    public class LoginViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly AuthService _userService;

        private MainViewModel mainViewModel;
        private string _email;
        private string _password = "";
        private Dictionary<string, bool> _dirtyProperties = new Dictionary<string, bool>();

        public ICommand CloseApplicationCommand { get; set; }
        public ICommand SwitchToRegistrationCommand { get; }
        public ICommand SignInCommand { get; set; }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    MarkAsDirty(nameof(Email));
                    RaisePropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    MarkAsDirty(nameof(Password));
                    RaisePropertyChanged();
                }
            }
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

                switch (columnName)
                {
                    case nameof(Email):
                        return ValidateEmail();
                    case nameof(Password):
                        if (Error != null)
                        {
                            return Error;
                        }
                        return null;
                    default:
                        return null;
                }
            }
        }

        private void MarkAsDirty(string propertyName)
        {
            _dirtyProperties[propertyName] = true;
        }

        private bool IsPropertyDirty(string propertyName)
        {
            return _dirtyProperties.ContainsKey(propertyName) && _dirtyProperties[propertyName];
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

        public LoginViewModel(MainViewModel mainViewModel, TravelSystemDbContext dbContext)
        {
            _userService = new AuthService(dbContext);

            this.mainViewModel = mainViewModel;
            SwitchToRegistrationCommand = new RelayCommand(_ =>
            {
                SwitchToRegistration();
            });
            CloseApplicationCommand = new RelayCommand(_ =>
            {
                CloseApplication();
            });
            SignInCommand = new RelayCommand(_ =>
            {
                SignIn();
            }, _ =>
            {
                return CanSignIn();
            });

            _dirtyProperties = new Dictionary<string, bool>
            {
                { nameof(Email), false },
            };
        }

        private void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        private void SignIn()
        {
            User user = _userService.Login(Email, Password);
            if (user != null)
            {
                if (user is Client)
                {
                    var w = new TripMarketView();
                    w.Show();
                } 
                else if (user is Agent)
                {
                    var w = new TripView();
                    w.Show();
                }
            } 
            else
            {
                Error = "Wrong email or password!";
                RaisePropertyChanged(nameof(Password));
                Error = null;
            }
        }

        private bool CanSignIn()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

        private void SwitchToRegistration()
        {
            mainViewModel.ShowRegistrationView();
        }
    }
}
