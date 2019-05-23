using Hangfire.Dashboard;

namespace dvt.webapp.Helpers
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return true; //httpContext.User.Identity.IsAuthenticated;
        }
    }
}
