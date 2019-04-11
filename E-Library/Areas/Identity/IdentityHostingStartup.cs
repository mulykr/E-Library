using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(LiBook.Areas.Identity.IdentityHostingStartup))]
namespace LiBook.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}