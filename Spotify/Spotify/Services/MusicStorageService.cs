using Amazon.S3.Transfer;
using Amazon.S3;
using Amazon.S3.Model;

namespace Spotify.Services;

public class MusicStorageService : IMusicStorageService
{
  private static IAmazonS3? _s3Client;
  private readonly TransferUtility _fileTransferUtility;
  
  private const string BucketName = "spotify-mahdavi";
  private const string ObjectName = "<OBJECT_NAME>";
  private static string _localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

  public MusicStorageService()
  {
      var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("53940f0c-4b40-442b-bed0-e8025f6d3463",
          "8c6ec26471b8a2eb21de140e601445e8df750f0d89a744b736154a85b1a35eaa");
      var config = new AmazonS3Config { ServiceURL = "https://spotify-mahdavi.s3.ir-thr-at1.arvanstorage.ir" };
      _s3Client = new AmazonS3Client(awsCredentials, config);
      _fileTransferUtility = new TransferUtility(_s3Client);
  }

  public void UploadMusic(string filePath, string keyName)
  {
      //Upload file to Amazon S3
      _fileTransferUtility.Upload(filePath, BucketName, keyName);
      Console.WriteLine("Upload Completed!");
  }
  
  public string UploadObjectFromFileAsync(IFormFile file, string keyName)
  {
      try
      {
          var request = new TransferUtilityUploadRequest()
          {
              BucketName = BucketName,
              Key = keyName,
              InputStream = file.OpenReadStream(),
              CannedACL = S3CannedACL.PublicRead
          };
          _fileTransferUtility.UploadAsync(request);
          Console.WriteLine("Upload Completed!");

          var url = $"https://{BucketName}.s3.ir-thr-at1.arvanstorage.ir/{keyName}";
          return url;
      }
      catch (AmazonS3Exception e)
      {
          Console.WriteLine($"Error: {e.Message}");
      }

      return null!;
  }

  public void DownloadMusic(string destinationFilePath, string keyName)
  {
      ReadObjectDataAsync(BucketName, keyName, destinationFilePath).Wait();
  }
  
  public async Task ReadObjectDataAsync(string bucketName, string keyName, string destinationFilePath)
  {
      try
      {
          GetObjectRequest request = new GetObjectRequest
          {
              BucketName = bucketName,
              Key = keyName,
          };

          using var response = await _s3Client!.GetObjectAsync(request);
          await using var responseStream = response.ResponseStream;
          using (var fileStream = new FileStream(destinationFilePath, FileMode.Create))
          {
              // Copy the object's stream to the file stream
              await responseStream.CopyToAsync(fileStream);

              Console.WriteLine("File successfully downloaded");
          }
      }
      catch (AmazonS3Exception e)
      {
          Console.WriteLine($"Error: '{e.Message}'");
      }
  }
  
}



