using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebDeApplication.Models
{

    public class DashboardData
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double TotalProfit { get; set; }
        public double TotalNetProfit { get; set; }

        public double PercentProfit { get; set; }
        public int TotalOrder { get; set; }
        public float PercentOrder { get; set; }

        public int TotalCancel { get; set; }
        public float PercentCancel { get; set; }
        public int TotalDelay { get; set; }
        public float PercentDelay { get; set; }

        public string SiteName { get; set; }

    }
}
