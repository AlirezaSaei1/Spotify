namespace Spotify.Services;

public interface IMusicStorageService
{
    void UploadMusic(string filePath, string keyName);
    string UploadObjectFromFileAsync(IFormFile file, string keyName);
    void DownloadMusic(string destinationFilePath, string keyName);
    Task ReadObjectDataAsync(string bucketName, string keyName, string destinationFilePath);
}