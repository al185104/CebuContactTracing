using CebuContactTracing.Models;
using CebuContactTracing.Services.Navigation;
using CebuContactTracing.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CebuContactTracing.ViewModels.Popup
{
    class CommonPopUpViewModel : ViewModelBase
    {
        private PopupModel _popup;
        private INavigationService _navigationService;
        private string _animation;
        private bool _isExiting;
        private bool isOkVisible;
        private bool isProcessFooterVisible;
        private string celebration;

        public string Celebration
        {
            get { return celebration; }
            set { celebration = value; RaisePropertyChanged(() => Celebration); }
        }

        public bool IsProcessFooterVisible
        {
            get { return isProcessFooterVisible; }
            set { isProcessFooterVisible = value; RaisePropertyChanged(() => IsProcessFooterVisible); }
        }

        public bool IsOkVisible
        {
            get { return isOkVisible; }
            set { isOkVisible = value; RaisePropertyChanged(() => IsOkVisible); }
        }

        public string Animation
        {
            get { return _animation; }
            set { _animation = value; RaisePropertyChanged(() => Animation); }
        }


        public PopupModel Popup
        {
            get { return _popup; }
            set { _popup = value; RaisePropertyChanged(() => Popup); }
        }

        #region Constructor
        public CommonPopUpViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        } 
        #endregion

        #region Override Method
        public async override Task InitializeAsync(object navigationData)
        {
            _isExiting = false;
            if (navigationData is PopupModel)
            {
                Popup = navigationData as PopupModel;

                // check animation
                if (string.IsNullOrEmpty(_popup.Animation))
                    Animation = "check.json";
                else
                    Animation = _popup.Animation;
                // celebration animation
                if (_popup.IsCelebration)
                    Celebration = "celebration.json";
                // okbutton control
                if(!string.IsNullOrEmpty(_popup.OKButtonLabel))
                    IsOkVisible = true;
                // processing control
                if(_popup.IsProcessFooterVisible)
                    isProcessFooterVisible = true;
                // screen delay
                if(_popup.Delay != 0)
                {
                    await Task.Delay(_popup.Delay);
                    // auto exit
                    await GoBack();
                }
            }
        }
        #endregion

        #region Commands
        public ICommand OKCommand => new Command(async () => await ExecuteOKCommand());
        private async Task ExecuteOKCommand()
        {
            await GoBack();
        }

        private async Task GoBack()
        {
            if(!_isExiting)
            {
                _isExiting = true;

                if(_popup.ToExit)
                {
                    await _navigationService.RemoveBackStackAsync();
                    await _navigationService.RemoveBackStackAsync(true);

                    await _navigationService.NavigateToAsync<MainPageViewModel>();
                }
                else
                    await _navigationService.RemoveLastFromBackStackAsync(true);
            }
        }
        #endregion
    }
}
