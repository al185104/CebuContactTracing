using CebuContactTracing.Models;
using CebuContactTracing.Services.CCT;
using CebuContactTracing.Services.Navigation;
using CebuContactTracing.ViewModels.Base;
using CebuContactTracing.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CebuContactTracing.ViewModels
{
    class MarshalPageViewModel : ViewModelBase
    {
        private string result;
        private string error;
        private string resultColor;
        private bool outputPanel;
        private string toggleString = "CHECK IN";
        private bool toggleCheckIn = true;

        public bool ToggleCheckIn
        {
            get { return toggleCheckIn; }
            set 
            { 
                toggleCheckIn = value; 
                RaisePropertyChanged(() => ToggleCheckIn);

                if (toggleCheckIn)
                    ToggleString = "CHECK IN";
                else
                    ToggleString = "CHECK OUT";
            }
        }

        public string ToggleString
        {
            get { return toggleString; }
            set { toggleString = value; RaisePropertyChanged(() => ToggleString); }
        }

        public bool OutputPanel
        {
            get { return outputPanel; }
            set { outputPanel = value; RaisePropertyChanged(() => OutputPanel); }
        }

        public string ResultColor
        {
            get { return resultColor; }
            set { resultColor = value; RaisePropertyChanged(() => ResultColor); }
        }

        public string Error
        {
            get { return error; }
            set { error = value; RaisePropertyChanged(() => Error); }
        }

        public string Result
        {
            get { return result; }
            set { result = value; RaisePropertyChanged(() => Result); }
        }
        
        private INavigationService _navigationService;
        private ICCTService _icctService;

        public MarshalPageViewModel(INavigationService navigationService, ICCTService iCCTService)
        {
            _navigationService = navigationService;
            _icctService = iCCTService;

            MessagingCenter.Unsubscribe<MarshalPageView, string>(this, MessageKeys.QRCheckInOut);
            MessagingCenter.Subscribe<MarshalPageView, string>(this, MessageKeys.QRCheckInOut, async (sender, arg) =>
            {
                ActivityModel activity = new ActivityModel();
                activity.familyCode = arg;
                activity.placeCode = "23"; // todo - consolidate with the team. static for now.
                activity.user_id = 6; // todo

                var ret = await _icctService.PostCheckInOutAsync(activity, toggleCheckIn);

                OutputPanel = true;
                if (ret.success)
                {
                    Result = "passed.json";
                    Error = "";
                }
                else
                {
                    Result = "failed.json";
                    Error = ret.errorCode + "\nError: You are currently checked-in somewhere else";
                }
                //if (arg.Equals("BUL50A41"))
                //{
                //    Result = "passed.json";
                //    Error = "";
                //}
                //else
                //{
                //    Result = "failed.json";
                //    Error = "Error: you are currently checked-in at citi hardware.";
                //}
            });
        }
        public async override Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            //var ret = await _icctService.GetPlaceListAsync();
            //List<PlaceResponseModel> p = JsonConvert.DeserializeObject<List<PlaceResponseModel>>(ret.data.ToString());
            IsBusy = false;
        }
    }
}