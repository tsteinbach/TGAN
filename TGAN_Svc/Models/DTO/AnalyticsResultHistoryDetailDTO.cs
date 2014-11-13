using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TGAN_Svc.Models.DTO
{
    public class AnalyticsResultHistoryDetailDTO
    {
        public string Season { get; set; }
        public string ResultFirstHalfSeason { get; set; }
        public string ResultSecondHalfSeason { get; set; }
    }
}