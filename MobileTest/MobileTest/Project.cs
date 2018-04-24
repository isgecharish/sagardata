using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileTest
{
    public class Project
    {
        public string Activity { get; set; }
        //private DateTime scdt;
        public DateTime ScheduledFinishedDate { get; set; }
        public DateTime ScheduledStartDate { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualFinishDate { get; set; }
        public string sRemarks { get; set; }
        public string sProjName { get; set; }
        public string ActivityID { get; set; }
    }
}