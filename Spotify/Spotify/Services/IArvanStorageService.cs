namespace Spotify.Services;

public interface IArvanStorageService
{
    Task MainUpload();
    Task MainDownload();
}