using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebApiNetCore.Services;

namespace WebApiNetCore.Middleware
{
    public static class MiddlewareExtensions
    {
        public static async void AddSeedData(this IApplicationBuilder app)
        {
            var seedDataService = app.ApplicationServices.GetRequiredService<ISeedDataService>();
            seedDataService.EnsureSeedData();
        }

        public static IApplicationBuilder ApplySecretKeyValidation(this IApplicationBuilder app)
        {
            app.UseMiddleware<SecretKeyValidatorsMiddleware>();
            return app;
        }

        public static IApplicationBuilder ErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            return app;
        }
    }
}