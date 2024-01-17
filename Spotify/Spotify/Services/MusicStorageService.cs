using Amazon.S3.Transfer;
using Amazon.S3;

namespace Spotify.Services;

public class MusicStorageService : IMusicStorageService
{
    private readonly TransferUtility _fileTransferUtility;
      
      private const string BucketName = "spotify-mahdavi";

      public MusicStorageService()
      {
          var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("53940f0c-4b40-442b-bed0-e8025f6d3463",
              "8c6ec26471b8a2eb21de140e601445e8df750f0d89a744b736154a85b1a35eaa");
          var config = new AmazonS3Config { ServiceURL = "https://s3.ir-thr-at1.arvanstorage.ir" };
          IAmazonS3 s3Client = new AmazonS3Client(awsCredentials, config);
          _fileTransferUtility = new TransferUtility(s3Client);
      }

      // public void UploadMusic(string filePath, string keyName)
      // {
      //     //Upload file to Amazon S3
      //     _fileTransferUtility.Upload(filePath, BucketName, keyName);
      //     Console.WriteLine("Upload Completed!");
      // }
      
      public string UploadObjectFromFile(IFormFile file, string keyName)
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
              _fileTransferUtility.Upload(request);
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
}