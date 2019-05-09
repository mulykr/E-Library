using LiBook.Services.Interfaces;

namespace LiBook.Services
{
    public class AppConfiguration : IAppConfiguration
    {
        public AppConfiguration(string webRootPath, string extensions)
        {
            WebRootPath = webRootPath;
            AllowedFileExtensions = extensions.Split(',');
        }

        public string WebRootPath { get; set; }
        public string[] AllowedFileExtensions { get; set; }
    }
}
