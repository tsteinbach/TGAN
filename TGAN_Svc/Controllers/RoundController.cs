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
    public class RoundController : ApiController
    {
        // GET api/round
        public IEnumerable<RoundDTO> Get()
        {
            using (var analytics = new Models.TGANAnalyticsEntities())
            {
                var result = analytics.RoundsOfActualSeasons.Select(x => new RoundDTO { 
                    ID = x.ID,
                    RoundNo = x.Spieltag
                });

                return result.ToList();
            }
        }
    }
}