using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public static class Utilities
    {
        public static Users getUserId(ClaimsPrincipal User)
        {
            var userId = Convert.ToInt32( User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).First() );
            var applicationId = Convert.ToInt32(User.Claims.Where(c => c.Type == "ApplicationID").Select(c => c.Value).First());
            return new Users { 
            Id = userId,
            ApplicationId = applicationId
            };
        }
    }
}
