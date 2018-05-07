using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace WebApiNetCore
{
    public class AuthenticationFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        private string secrets = "Secret007";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out Microsoft.Extensions.Primitives.StringValues value);
            if (!(value.Count > 0 && value.SingleOrDefault().Equals(secrets)))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}