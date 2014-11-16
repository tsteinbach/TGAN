using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TGAN_Svc.Models.DTO
{
    public class AnalyticsTendencyDTO
    {
        public string User { get; set; }
        public string Season { get; set; }
        public string Tipp { get; set; }
        public int Count { get; set; }
        public List<AnalyticsTendencyDTO> SeasonGroup { get; set; }
        public List<AnalyticsTendencyDTO> UserGroup { get; set; }
    }
}