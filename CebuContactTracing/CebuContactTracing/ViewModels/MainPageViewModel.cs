using CebuContactTracing.Services.Navigation;
using CebuContactTracing.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CebuContactTracing.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        public string ContactNumber { get; set; }

        private INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        public ICommand SignInCommand => new Command(async () => await ExecuteSignInCommand());

        private async Task ExecuteSignInCommand()
        {
            if(!string.IsNullOrEmpty(ContactNumber))
                await _navigationService.NavigateToAsync<AccountPageViewModel>();
            else
                await _navigationService.NavigateToAsync<MarshalPageViewModel>();
        }
        public ICommand RegistrationCommand => new Command(async () => await ExecuteRegistrationCommand());

        private async Task ExecuteRegistrationCommand()
        {
            await _navigationService.NavigateToAsync<RegistrationPageViewModel>();
        }
    }
}
