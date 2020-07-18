using CebuContactTracing.Services.Navigation;
using CebuContactTracing.ViewModels.Base;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CebuContactTracing
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected async override void OnStart()
        {
            base.OnStart();
            await InitNavigation();
            base.OnResume();
        }
        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
