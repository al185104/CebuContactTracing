using CebuContactTracing.Models;
using CebuContactTracing.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CebuContactTracing.ViewModels
{
    class NewsContentPageViewModel : ViewModelBase
    {
        private NewsModel news;

        public NewsModel News
        {
            get { return news; }
            set { news = value; RaisePropertyChanged(() => News); }
        }


        public NewsContentPageViewModel()
        {

        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData != null && navigationData is NewsModel)
                News = navigationData as NewsModel;
            return base.InitializeAsync(navigationData);
        }

        public ICommand GotoSourceCommand => new Command(async () => await ExecuteGotoSourceCommand());

        private async Task ExecuteGotoSourceCommand()
        {
            await Browser.OpenAsync(news.url, BrowserLaunchMode.SystemPreferred);
        }
    }
}
