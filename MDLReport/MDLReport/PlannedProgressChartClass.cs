using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDLReport
{
    public class PlannedProgressChartClass
    {
        public Decimal TotalCount { get; set; }
        public Decimal BaselinePlannedPerc { get; set; }
        public Decimal RevisedPlannedPerc { get; set; }
        public Decimal ActualPerc { get; set; }
        public string BaselinePlannedPer { get; set; }
        public string RevisedPlannedPer { get; set; }
        public string ActualPer { get; set; }
        public int slNo { get; set; }
        public string Months { get; set; }
    }
}