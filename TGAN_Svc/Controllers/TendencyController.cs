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

        // GET api/Tendency/Get/475A6D64-7306-4FDA-8DA6-CDC733BA0A71
        public IEnumerable<AnalyticsTendencyDTO> Get(string id)
        {
            using (var analytics = new Models.TGANAnalyticsEntities())
            {
                var result = analytics.AnalyticsTendency(new Guid(id)).Select(x => new AnalyticsTendencyDTO
                {
                    User = x.User,
                    Season = x.Season,
                    Tipp = x.Tipp,
                    Count = x.Count_Tendenz.HasValue ? x.Count_Tendenz.Value : 0
                }).ToList();

                List<AnalyticsTendencyDTO> response = new List<AnalyticsTendencyDTO>();

                foreach (var tippGroup in result.GroupBy(g => g.Tipp))
                {
                    var group = new AnalyticsTendencyDTO
                    {
                        Tipp = tippGroup.First().Tipp,
                        Count = tippGroup.Sum(x => x.Count)
                    };

                    group.SeasonGroup = new List<AnalyticsTendencyDTO>();

                    foreach (var seasonGroup in tippGroup.GroupBy(x => x.Season))
	                {
                        var season = new AnalyticsTendencyDTO
                        {
                            Tipp = seasonGroup.First().Tipp,
                            Season = seasonGroup.First().Season,
                            Count = seasonGroup.Sum(x => x.Count)
                        };

                        season.UserGroup = new List<AnalyticsTendencyDTO>();

                        foreach (var userGroup in seasonGroup.GroupBy(x => x.User))
                        {
                            season.UserGroup.Add( new AnalyticsTendencyDTO
                            {
                                Tipp = userGroup.First().Tipp,
                                Season = userGroup.First().Season,
                                User = userGroup.First().User,
                                Count = userGroup.Sum(x => x.Count)
                            });

                        }

                        group.SeasonGroup.Add(season);
	                } 

                    response.Add(group);
                } 


                return response;
            }
        }
    }
}