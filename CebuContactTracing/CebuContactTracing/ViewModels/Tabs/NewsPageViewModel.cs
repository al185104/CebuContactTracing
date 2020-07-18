using CebuContactTracing.Models;
using CebuContactTracing.Services.Navigation;
using CebuContactTracing.Services.Statistics;
using CebuContactTracing.ViewModels.Base;
using CebuContactTracing.ViewModels.Popup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CebuContactTracing.ViewModels.Tabs
{
    class NewsPageViewModel : ViewModelBase
    {
        private NewsCollection news = new NewsCollection();
        private IStatisticsService _statisticsService;
        private INavigationService _navigationService;

        public NewsCollection News
        {
            get { return news; }
            set { news = value; RaisePropertyChanged(()=>News); }
        }

        public NewsPageViewModel(IStatisticsService statisticsService, INavigationService navigationService)
        {
            _statisticsService = statisticsService;
            _navigationService = navigationService;

            GetNews();
        }

        private async Task GetNews()
        {
            var n = await _statisticsService.GetNewsAsync();
            News = n;
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        public ICommand ViewNewsCommand => new Command(async (obj) => await ExecuteViewNewsCommand(obj));

        private async Task ExecuteViewNewsCommand(object obj)
        {
            await _navigationService.NavigateToAsync<NewsContentPageViewModel>(obj);
        }
    }
}
