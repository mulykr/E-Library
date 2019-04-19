using LiBook.Services.Interfaces;

namespace LiBook.Services
{
    public class AppConfiguration : IAppConfiguration
    {
        public AppConfiguration(string webRootPath)
        {
            WebRootPath = webRootPath;
        }

        public string WebRootPath { get; set; }
    }
}
