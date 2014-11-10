using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TGAN_Svc.Models.DTO
{
    public class AnalyticsUnechteDTO
    {
        public string User { get; set; }
        public string Season { get; set; }
        public int Count { get; set; }
    }
}