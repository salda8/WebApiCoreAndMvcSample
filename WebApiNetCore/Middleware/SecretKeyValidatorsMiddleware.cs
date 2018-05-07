
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WebApiNetCore.Repositories;

namespace WebApiNetCore.Middleware
{
    public class SecretKeyValidatorsMiddleware
    {
        private readonly RequestDelegate _next;
        private ISecretRepository ContactsRepo { get; set; }

        public SecretKeyValidatorsMiddleware(RequestDelegate next, ISecretRepository _repo)
        {
            _next = next;
            ContactsRepo = _repo;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.Keys.Contains("x-api-key"))
            {
                context.Response.StatusCode = 400; //Bad Request                
                await context.Response.WriteAsync("User Key is missing");
                return;
            }
            else
            {
                if (!ContactsRepo.CheckValidUserSecret(context.Request.Headers["x-api-key"]))
                {
                    context.Response.StatusCode = 401; //UnAuthorized
                    await context.Response.WriteAsync("Invalid User Key");
                    return;
                }
            }

            await _next.Invoke(context);
        }

    }

   
  
}