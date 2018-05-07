using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebMvcNetCore
{
    public class Program
    {
        public static void Main(string[] args) => BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                // .ConfigureAppConfiguration((context, config) =>
                //{
                //    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                //})
                .Build();
    }
}