namespace Spotify.Services;

public interface IMusicStorageService
{
    string UploadObjectFromFile(IFormFile file, string keyName);
}