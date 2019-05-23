using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Dvt.Infrastructure.Enums;
using Dvt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using wtw.webapp.Helpers;

namespace dvt.webapp.AppCode
{
    public class WebClaimsPrincipal : IPrincipal
    {
        // constructor used by background jobs
        public WebClaimsPrincipal()
        {

        }

        public WebClaimsPrincipal(IHttpContextAccessor accessor)
        {
        //    var appIdentity = accessor.HttpContext.User.Identities.LastOrDefault();

        //    FirstName = appIdentity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ?? "Unknown";

        //    EmailAddress = appIdentity?.Claims.FirstOrDefault(x => x.Type == ClaimType.Email)?.Value;

        //    Id = Convert.ToInt32(appIdentity?.Claims.FirstOrDefault(x => x.Type == ClaimType.UserId)?.Value);

        //    var systemFunctions = appIdentity?.Claims.FirstOrDefault(x => x.Type == ClaimType.SystemFunctions)?.Value;
        //    SystemFunctions = new List<SystemFunction>();
        //    if (systemFunctions != null)
        //    {
        //        foreach (var systemFuction in systemFunctions.Split(";"))
        //        {
        //            SystemFunctions.Add((SystemFunction)int.Parse(systemFuction));
        //        }
        //    }
        }

        public string FirstName { get; }
        public string EmailAddress { get; }
        public int Id { get; set; }
        public IList<SystemFunction> SystemFunctions { get; }
    }

}
