namespace LiBook.Services.Interfaces
{
    public interface IAppConfiguration
    {
        string WebRootPath { get; set; }
        string[] AllowedFileExtensions { get; set; }
    }
}
