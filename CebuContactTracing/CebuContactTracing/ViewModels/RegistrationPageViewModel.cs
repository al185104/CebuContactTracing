using CebuContactTracing.Models;
using CebuContactTracing.Services.CCT;
using CebuContactTracing.Services.Navigation;
using CebuContactTracing.ViewModels.Base;
using CebuContactTracing.ViewModels.Popup;
using CebuContactTracing.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace CebuContactTracing.ViewModels
{
    class RegistrationPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private ICCTService _icctService;
        private BarangayListModel barangayList;
        private FamilyModel familyList = new FamilyModel();

        public FamilyModel FamilyList
        {
            get { return familyList; }
            set { familyList = value; RaisePropertyChanged(() => FamilyList); }
        }

        public BarangayModel SelectedBarangay { get; set; }

        public BarangayListModel BarangayList
        {
            get { return barangayList; }
            set { barangayList = value; RaisePropertyChanged(() => BarangayList); }
        }
        
        public RegistrationPageViewModel(INavigationService navigationService, ICCTService iCCTService)
        {
            _navigationService = navigationService;
            _icctService = iCCTService;
            MessagingCenter.Unsubscribe<RegistrationPageView, string>(this, MessageKeys.QRScanned);
            MessagingCenter.Subscribe<RegistrationPageView, string>(this, MessageKeys.QRScanned, async (sender, arg) =>
            {
                FamilyList.familyCode = arg;
                RaisePropertyChanged(() => FamilyList);
            });
        }

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;
            BarangayList = await _icctService.GetBarangayListAsync();
            FamilyList = new FamilyModel();
            FamilyList.familyMembers.Add(new FamilyMember());
            IsBusy = false;
        }

        public ICommand AddFamilyCommand => new Command(() => ExecuteAddFamilyCommand());

        private void ExecuteAddFamilyCommand()
        {
            FamilyList.familyMembers.Insert(0, new FamilyMember());
            //FamilyList.familyMembers.Add(new FamilyMember());
            RaisePropertyChanged(() => FamilyList);
        }

        public ICommand RegisterCommand => new Command(async () => await ExecuteRegisterCommand());

        private async Task ExecuteRegisterCommand()
        {
            IsBusy = true;

            if(FamilyList != null)
            {
                familyList.barangay_id = SelectedBarangay.id;
                var result = await _icctService.PostFamilyAsync(familyList);
                if (result.success)
                {
                    await _navigationService.NavigateToPopUpAsync<CommonPopUpViewModel>(new PopupModel()
                    {
                        ToExit = true,
                        Delay = 15000,
                        OKButtonLabel = "Okay",
                        Title = "Registration Sent",
                        Head = "You have successfully sent registration request.",
                        Body = "Please wait for the SMS verification from your respective Barangay."
                    });
                }
                else
                {
                    await _navigationService.NavigateToPopUpAsync<CommonPopUpViewModel>(new PopupModel()
                    {
                        ToExit = true,
                        Animation = "cross.json",
                        Delay = 15000,
                        OKButtonLabel = "Back",
                        Title = "Registration Failed",
                        Head = "Something went wrong!",
                        Body = "Error Code: " + result.errorCode.ToString()
                    });
                }
                FamilyList = new FamilyModel();
            }

            IsBusy = false;
        }

        private void SetDummyValues()
        {
            familyList.address = "a";
            familyList.contactNumber = "3333333333";
            familyList.familyCode = "BUL88A88";
            familyList.registrationType = "TYPE_1";
            familyList.barangay_id = 12;

            familyList.familyMembers.Add(new FamilyMember
            {
                age = 10,
                firstName = "b",
                middleName = "b",
                lastName = "b",
                gender = "m",
                password = null,
                username = null
            });
        }
    }
}
