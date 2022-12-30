using System;
using System.Collections.Generic;
using Radiostation.DAL.Entities;

namespace Radiostation.WebUI.Models
{
    public class DashboardViewModel
    {
        public DateTime YesterdayDate { get; set; }

        public List<Translation> Yesterday { get; set; }

        public DateTime TodayDate { get; set; }

        public List<Translation> Today { get; set; }

        public DateTime TomorrowDate { get; set; }

        public List<Translation> Tomorrow { get; set; }
    }
}