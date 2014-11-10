using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TGAN_Svc.Models;

namespace TGAN_Svc.Controllers
{
    public class GroupController : ApiController
    {
        // GET api/group
        public IEnumerable<UserGroupDTO> Get()
        {
            using (var analytics = new Models.TGANAnalyticsEntities())
            {
                var result = analytics.UserGroups.Select(x => new UserGroupDTO { 
                    ID = x.ID,
                    Name = x.Name
                });

                return result.ToList();
            }
        }
    }
}