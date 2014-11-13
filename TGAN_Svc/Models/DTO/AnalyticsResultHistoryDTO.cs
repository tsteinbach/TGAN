using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TGAN_Svc.Models.DTO
{
    public class AnalyticsResultHistoryDTO
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int Team1Win { get; set; }
        public int Team2Win { get; set; }
        public int Draw { get; set; }
        public List<AnalyticsResultHistoryDetailDTO> Details { get; set; }
     
    }
}