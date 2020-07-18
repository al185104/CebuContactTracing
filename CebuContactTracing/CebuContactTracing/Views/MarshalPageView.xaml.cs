using CebuContactTracing.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace CebuContactTracing.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarshalPageView : ContentPage
    {
        public MarshalPageView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private void OpenScanner(object sender, EventArgs e)
        {
            Scanner();
        }

        public async void Scanner()
        {

            var ScannerPage = new ZXingScannerPage();

            ScannerPage.OnScanResult += (result) => {
                ScannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopAsync();
                    MessagingCenter.Send(this, MessageKeys.QRCheckInOut, result.Text);
                });
            };

            await Navigation.PushAsync(ScannerPage);
        }

        private void scanView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() => {
                //Navigation.PopAsync();
                MessagingCenter.Send(this, MessageKeys.QRCheckInOut, result.Text);
            });
        }
    }
}