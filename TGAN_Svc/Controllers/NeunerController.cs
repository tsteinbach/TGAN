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
    public class NeunerController : ApiController
    {
        // GET api/values
        public IEnumerable<AnalyticsNeunerDTO> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/neuner/475A6D64-7306-4FDA-8DA6-CDC733BA0A71
        public IEnumerable<AnalyticsNeunerDTO> Get(string id)
        {
            using (var analytics = new Models.TGANAnalyticsEntities())
            {
                var result = analytics.AnalyticsNeuner(new Guid(id)).Select(x => new AnalyticsNeunerDTO { 
                    User = x.User,
                    Season = x.Season,
                    Count = x.Count_Neuner.HasValue ? x.Count_Neuner.Value : 0
                });

                return result.ToList();
            }
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}