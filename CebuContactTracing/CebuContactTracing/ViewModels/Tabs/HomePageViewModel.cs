using CebuContactTracing.Models;
using CebuContactTracing.Services.CCT;
using CebuContactTracing.Services.Navigation;
using CebuContactTracing.Validations;
using CebuContactTracing.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CebuContactTracing.ViewModels.Tabs
{
    class HomePageViewModel : ViewModelBase
    {
        public LoginModel Login { get; set; } = new LoginModel();
        private INavigationService _navigationService;
        private ICCTService _cctService;
        private ValidatableObject<string> username;
        private ValidatableObject<string> password;

        public ValidatableObject<string> Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(() => Password); }
        }

        public ValidatableObject<string> Username
        {
            get { return username; }
            set { username = value; RaisePropertyChanged(() => Username); }
        }

        public HomePageViewModel(INavigationService navigationService, ICCTService iCCTService)
        {
            _navigationService = navigationService;
            _cctService = iCCTService;
            
            Username = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            AddValidations();
        }
        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        public ICommand SignInCommand => new Command(async () => await ExecuteSignInCommand());

        private async Task ExecuteSignInCommand()
        {
            IsBusy = true;
            
            if(Validate())
            {
                Login.username = "marshall_michael";
                Login.password = "mikoypikoy123";
                var login = await _cctService.PostLoginAsync(Login);

                if (login.success)
                    await _navigationService.NavigateToAsync<MarshalPageViewModel>();
            }

            IsValid = true;
            IsBusy = false;
        }
        public ICommand RegistrationCommand => new Command(async () => await ExecuteRegistrationCommand());

        private async Task ExecuteRegistrationCommand()
        {
            await _navigationService.NavigateToAsync<RegistrationPageViewModel>();
        }

        #region Validation
        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());

        public ICommand ValidatePasswordCommand => new Command(() => ValidatePassword());
        private bool _isValid;
        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }
        private bool Validate()
        {
            bool isValidUser = ValidateUserName();
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;
        }

        private bool ValidateUserName()
        {
            return Username.Validate();
        }

        private bool ValidatePassword()
        {
            return Password.Validate();
        }

        private void AddValidations()
        {
            Username.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        } 
        #endregion
    }
}
