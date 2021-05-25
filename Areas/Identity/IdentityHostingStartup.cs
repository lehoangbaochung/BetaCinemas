using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BetaCinemas.Areas.Identity.IdentityHostingStartup))]
namespace BetaCinemas.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}