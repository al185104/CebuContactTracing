using CebuContactTracing.Models;
using CebuContactTracing.Services.Statistics;
using CebuContactTracing.ViewModels.Base;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CebuContactTracing.ViewModels.Tabs
{
    class StatisticsPageViewModel : ViewModelBase
    {
        private List<string> chartColors = new List<string>()
        { "#CE7DA5", "#FFD1BA", "#493657", "#C5D86D", "#261C15", "#F05D23", "#E4E6C3" };

        #region Properties
        private StatisticsModel statisticsModel = new StatisticsModel();
        private string chartLabel = "Confirmed Cases";

        public string ChartLabel
        {
            get { return chartLabel; }
            set { chartLabel = value; RaisePropertyChanged(() => ChartLabel); }
        }

        public StatisticsModel StatisticsModel
        {
            get { return statisticsModel; }
            set { statisticsModel = value; RaisePropertyChanged(()=>StatisticsModel); }
        }

        private Chart casesChart;

        public Chart CasesChart
        {
            get { return casesChart; }
            set { casesChart = value; RaisePropertyChanged(() => CasesChart); }
        }

        private Chart dailyUpdateChart;
        private IStatisticsService _statisticsService;

        public Chart DailyUpdateChart
        {
            get { return dailyUpdateChart; }
            set { dailyUpdateChart = value; RaisePropertyChanged(() => DailyUpdateChart); }
        } 
        #endregion

        public StatisticsPageViewModel(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
            GetCharts();
        }

        public async Task GetCharts()
        {
            IsBusy = true;

            var stats = await _statisticsService.GetStatisticsAsync();
            var timeline = await _statisticsService.GetTimelineAsync();
            await DecorateChart(stats, timeline);

            IsBusy = false;
        }

        private Task DecorateChart(StatisticsCollection stats, List<TimelineModel> timeline)
        {
            #region BarChart
            foreach (var s in stats.data)
            {
                if (s.RegionName.Equals("Central Visayas"))
                {
                    statisticsModel.DeceasedCount = s.DeceasedCount;
                    statisticsModel.CasesCount = s.CasesCount;
                    statisticsModel.RecoveredCount = s.RecoveredCount;
                    statisticsModel.RegionName = s.RegionName;
                    RaisePropertyChanged(() => StatisticsModel);
                    break;
                }
            }

            for (int l = timeline.Count - 8; l < timeline.Count; l++)
            {
                LineConfirmed.Add(new Microcharts.Entry(timeline[l].Confirmed) { 
                    Color = SKColor.Parse(chartColors[l%7]), 
                    ValueLabel= timeline[l].Confirmed.ToString("#,#", CultureInfo.InvariantCulture),
                    Label = timeline[l].Date.Replace("-2020", string.Empty)
                });

                LineDiseased.Add(new Microcharts.Entry(timeline[l].Deaths) { 
                    Color = SKColor.Parse(chartColors[l % 7]), 
                    ValueLabel = timeline[l].Deaths.ToString("#,#", CultureInfo.InvariantCulture),
                    Label = timeline[l].Date.Replace("-2020", string.Empty)
                });

                LineRecovered.Add(new Microcharts.Entry(timeline[l].Recovered) { 
                    Color = SKColor.Parse(chartColors[l % 7]), 
                    ValueLabel = timeline[l].Recovered.ToString("#,#", CultureInfo.InvariantCulture),
                    Label = timeline[l].Date.Replace("-2020", string.Empty)
                });
            }

            DailyUpdateChart = new BarChart()
            {
                Entries = LineConfirmed,
                LabelTextSize = 40,
                BackgroundColor = SKColors.Transparent
            }; 
            #endregion

            return Task.CompletedTask;
        }

        #region Commands
        public ICommand ConfirmedCommand => new Command(() => ExecuteConfirmedCommand());
        public ICommand RecoveredCommand => new Command(() => ExecuteRecoveredCommand());
        public ICommand DiseasedCommand => new Command(() => ExecuteDiseasedCommand());

        public List<Microcharts.Entry> LineConfirmed { get; private set; } = new List<Microcharts.Entry>();
        public List<Microcharts.Entry> LineRecovered { get; private set; } = new List<Microcharts.Entry>();
        public List<Microcharts.Entry> LineDiseased { get; private set; } = new List<Microcharts.Entry>();

        private void ExecuteDiseasedCommand()
        {
            ChartLabel = "Diseased Cases";
            DailyUpdateChart = new BarChart()
            {
                Entries = LineDiseased,
                LabelTextSize = 45,
                BackgroundColor = SKColors.Transparent
            };
        }

        private void ExecuteRecoveredCommand()
        {
            ChartLabel = "Recovered Cases";
            DailyUpdateChart = new BarChart()
            {
                Entries = LineRecovered,
                LabelTextSize = 45,
                BackgroundColor = SKColors.Transparent
            };
        }

        private void ExecuteConfirmedCommand()
        {
            ChartLabel = "Confirmed Cases";
            DailyUpdateChart = new BarChart()
            {
                Entries = LineConfirmed,
                LabelTextSize = 45,
                BackgroundColor = SKColors.Transparent
            };
        }
        #endregion
    }
}
