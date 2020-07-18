using CebuContactTracing.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CebuContactTracing.Services.Statistics
{
    interface IStatisticsService
    {
        Task<StatisticsCollection> GetStatisticsAsync();
        Task<List<TimelineModel>> GetTimelineAsync();
        Task<NewsCollection> GetNewsAsync();
    }
}
