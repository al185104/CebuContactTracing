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
    public partial class RegistrationPageView : ContentPage
    {
        public RegistrationPageView()
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
            ScannerPage.Title = "Scan the QR Code";
            ScannerPage.OnScanResult += (result) => {
                ScannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopAsync();
                    MessagingCenter.Send(this, MessageKeys.QRScanned, result.Text);
                });
            };

            await Navigation.PushAsync(ScannerPage);
        }
    }
}