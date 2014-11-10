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
    public class UnechteController : ApiController
    {

        // GET api/Unechte/475A6D64-7306-4FDA-8DA6-CDC733BA0A71
        public IEnumerable<AnalyticsUnechteDTO> Get(string id)
        {
            using (var analytics = new Models.TGANAnalyticsEntities())
            {
                var result = analytics.AnalyticsUnechte(new Guid(id)).Select(x => new AnalyticsUnechteDTO
                {
                    User = x.User,
                    Season = x.Season,
                    Count = x.Count_Unechte_Bank.HasValue ? x.Count_Unechte_Bank.Value : 0
                });

                return result.ToList();
            }
        }
    }
}