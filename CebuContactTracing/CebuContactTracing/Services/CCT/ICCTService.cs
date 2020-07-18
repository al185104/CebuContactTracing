using CebuContactTracing.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CebuContactTracing.Services.CCT
{
    interface ICCTService
    {
        /// <summary>
        /// Login-controller
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<CommonResponseModel> PostLoginAsync(LoginModel login);
        /// <summary>
        /// Barangay-controller
        /// </summary>
        /// <returns></returns>
        Task<BarangayListModel> GetBarangayListAsync();
        /// <summary>
        /// Family-controller. Add new family instance.
        /// </summary>
        /// <param name="familyModel"></param>
        /// <returns></returns>
        Task<CommonResponseModel> PostFamilyAsync(FamilyModel familyModel);
        /// <summary>
        /// Activity-controller. Check In.
        /// </summary>
        /// <param name="activityModel"></param>
        /// <returns></returns>
        Task<CommonResponseModel> PostCheckInOutAsync(ActivityModel activityModel, bool checkIn);
        Task<CommonResponseModel> GetPlaceListAsync();
    }
}
