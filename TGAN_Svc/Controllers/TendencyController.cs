using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TGAN_Svc.Models;
using TGAN_Svc.Models.DTO;

namespace TGAN_Svc.Controllers
{
    public class TendencyController : ApiController
    {

        // GET api/Tendency/475A6D64-7306-4FDA-8DA6-CDC733BA0A71
        public IEnumerable<AnalyticsTendencyDTO> Get(string id)
        {
            using (var analytics = new Models.TGANAnalyticsEntities())
            {
                var result = analytics.AnalyticsTendency(new Guid(id)).Select(x => new AnalyticsTendencyDTO {
                    User = x.User,
                    Season = x.Season,
                    Tipp = x.Tipp,
                    Count = x.Count_Tendenz.HasValue ? x.Count_Tendenz.Value : 0
                });

                return result.ToList();
            }
        }
    }
}