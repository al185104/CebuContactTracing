using CebuContactTracing.Helpers;
using CebuContactTracing.Models;
using CebuContactTracing.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CebuContactTracing.Services.Statistics
{
    class StatisticsService : IStatisticsService
    {
        private IRequestProvider _requestProvider;
        private const string ApiUrlBase = "https://www.cyberpurge.com/api/covid";
        private const string ApiUrlRegion = "/regionalDataByCountry/PH";
        //private const string ApiUrlTimelineBase = "https://covidapi.info/api/v1/country/PHL";
        private const string ApiUrlTimelineBase = "https://api-corona.azurewebsites.net/timeline/philippines";
        private const string ApiUrlNewsBase = "http://api.coronatracker.com/news/trending?limit=20&country=Philippines";

        public StatisticsService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }
        public async Task<StatisticsCollection> GetStatisticsAsync()
        {
            var uri = UriHelper.CombineUri(ApiUrlBase, $"{ApiUrlRegion}");

            StatisticsCollection stats;
            try
            {
                stats = await _requestProvider.GetAsync<StatisticsCollection>(uri, "X-Authorization", "");
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                stats = null;
            }

            return stats;
        }

        public async Task<List<TimelineModel>> GetTimelineAsync()
        {
            var uri = UriHelper.CombineUri(ApiUrlTimelineBase);

            List<TimelineModel> stats;
            try
            {
                stats = await _requestProvider.GetAsync<List<TimelineModel>>(uri);
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                stats = null;
            }

            return stats;
        }

        public async Task<NewsCollection> GetNewsAsync()
        {
            var uri = UriHelper.CombineUri(ApiUrlNewsBase);

            NewsCollection stats;
            try
            {
                stats = await _requestProvider.GetAsync<NewsCollection>(uri);
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                stats = null;
            }

            return stats;
        }
    }
}
