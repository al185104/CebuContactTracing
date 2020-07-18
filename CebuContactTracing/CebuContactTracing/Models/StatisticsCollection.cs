using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CebuContactTracing.Models
{
    public class StatisticsCollection
    {
        public bool success { get; set; }
        public ObservableCollection<StatisticsModel> data { get; set; } = new ObservableCollection<StatisticsModel>();
    }
}
