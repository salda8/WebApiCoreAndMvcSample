using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace WebApiNetCore
{
    public class AuthenticationFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        private const string Secrets = "Secret007";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out StringValues value);
            if (!(value.Count > 0 && value.SingleOrDefault().Equals(Secrets)))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}