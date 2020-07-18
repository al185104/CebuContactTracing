using CebuContactTracing.Helpers;
using CebuContactTracing.Models;
using CebuContactTracing.Services.RequestProvider;
using CebuContactTracing.Services.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CebuContactTracing.Services.CCT
{
    class CCTService : ICCTService
    {
        private IRequestProvider _requestProvider;
        private ISettingsService _settingsService;
        private const string ApiUrlLogin = "login-rest/login";
        private const string ApiUrlBarangayList = "barangay-rest/list";
        private const string ApiUrlFamilyAdd = "family-rest/add";
        private const string ApiUrlPlaceList = "place-rest/list";
        private const string ApiUrlCheckIn = "activity-rest/checkIn";
        private const string ApiUrlCheckOut = "activity-rest/checkOut";

        public CCTService(IRequestProvider requestProvider, ISettingsService settingsService)
        {
            _requestProvider = requestProvider;
            _settingsService = settingsService;
        }

        public async Task<CommonResponseModel> PostLoginAsync(LoginModel login)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"/{ApiUrlLogin}");

            CommonResponseModel stats;
            try
            {
                stats = await _requestProvider.PostAsync<CommonResponseModel>(uri, login);
                _settingsService.AuthAccessToken = stats.data.ToString();
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                stats = null;
            }

            return stats;
        }

        public async Task<BarangayListModel> GetBarangayListAsync()
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"/{ApiUrlBarangayList}");

            BarangayListModel stats;
            try
            {
                //stats = await _requestProvider.GetAsync<BarangayListModel>(uri, _settingsService.AuthAccessToken);
                stats = await _requestProvider.GetAsync<BarangayListModel>(uri);
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                stats = null;
            }

            return stats;
        }

        public async Task<CommonResponseModel> PostFamilyAsync(FamilyModel familyModel)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, ApiUrlFamilyAdd);
            var result = await _requestProvider.PostAsync<CommonResponseModel>(uri, familyModel);
            return result;
        }

        public async Task<CommonResponseModel> PostCheckInOutAsync(ActivityModel activityModel, bool checkIn)
        {
            string uri;
            if(checkIn)
                uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, ApiUrlCheckIn);
            else
                uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, ApiUrlCheckOut);
            
            var result = await _requestProvider.PostAsync<CommonResponseModel>(uri, activityModel, _settingsService.AuthAccessToken);
            return result;
        }

        public async Task<CommonResponseModel> GetPlaceListAsync()
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"/{ApiUrlPlaceList}");

            CommonResponseModel stats;
            try
            {
                stats = await _requestProvider.GetAsync<CommonResponseModel>(uri, _settingsService.AuthAccessToken);
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                stats = null;
            }

            return stats;
        }
    }
}
