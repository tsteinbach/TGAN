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
    public class EchteController : ApiController
    {

        // GET api/Echte/475A6D64-7306-4FDA-8DA6-CDC733BA0A71
        public IEnumerable<AnalyticsEchteDTO> Get(string id)
        {
            using (var analytics = new Models.TGANAnalyticsEntities())
            {
                var result = analytics.AnalyticsEchte(new Guid(id)).Select(x => new AnalyticsEchteDTO
                {
                    User = x.User,
                    Season = x.Season,
                    Count = x.Count_Echte_Bank.HasValue ? x.Count_Echte_Bank.Value : 0
                });

                return result.ToList();
            }
        }
    }
}