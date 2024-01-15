namespace Spotify.Storage;

// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier:  Apache - 2.0

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Threading.Tasks;
using System.Reflection;



  // The following example shows two different methods for uploading data to
  // an Amazon Simple Storage Service (Amazon S3) bucket. The method,
  // UploadObjectFromFileAsync, uploads an existing file to an Amazon S3
  // bucket. The method, UploadObjectFromContentAsync, creates a new
  // file containing the text supplied to the method. The application
  // was created using AWS SDK for .NET 3.5 and .NET 5.0.

public class ArvanStorage
{
  private static IAmazonS3 _s3Client;

  private const string BUCKET_NAME = "spotify";
  private const string OBJECT_NAME = "<OBJECT_NAME>";

  private static string LOCAL_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

  static async Task MainUpload()
  {
      var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("53940f0c-4b40-442b-bed0-e8025f6d3463",
          "8c6ec26471b8a2eb21de140e601445e8df750f0d89a744b736154a85b1a35eaa");
      var config = new AmazonS3Config { ServiceURL = "https://spotify-mahdavi.s3.ir-thr-at1.arvanstorage.ir" };
      _s3Client = new AmazonS3Client(awsCredentials, config);

      // The method expects the full path, including the file name.
      var path = $"{LOCAL_PATH}/{OBJECT_NAME}";

      await UploadObjectFromFileAsync(_s3Client, BUCKET_NAME, OBJECT_NAME, path);
  }

  /// <summary>
  /// This method uploads a file to an Amazon S3 bucket. This
  /// example method also adds metadata for the uploaded file.
  /// </summary>
  /// <param name="client">An initialized Amazon S3 client object.</param>
  /// <param name="bucketName">The name of the S3 bucket to upload the
  /// file to.</param>
  /// <param name="objectName">The destination file name.</param>
  /// <param name="filePath">The full path, including file name, to the
  /// file to upload. This doesn't necessarily have to be the same as the
  /// name of the destination file.</param>
  static async Task UploadObjectFromFileAsync(IAmazonS3 client, string bucketName, string objectName, 
      string filePath)
  {
      try
      {
          var putRequest = new PutObjectRequest
          {
              BucketName = bucketName,
              Key = objectName,
              FilePath = filePath,
              ContentType = "text/plain"
          };

          putRequest.Metadata.Add("x-amz-meta-title", "someTitle");

          PutObjectResponse response = await client.PutObjectAsync(putRequest);

          foreach (PropertyInfo prop in response.GetType().GetProperties())
          {
              Console.WriteLine($"{prop.Name}: {prop.GetValue(response, null)}");
          }

          Console.WriteLine($"Object {OBJECT_NAME} added to {bucketName} bucket");
      }
      catch (AmazonS3Exception e)
      {
          Console.WriteLine($"Error: {e.Message}");
      }
  }
  

  public static async Task MainDownload()
  {
      const string bucketName = "<BUCKET_NAME>";
      var keyName = OBJECT_NAME;

      var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("53940f0c-4b40-442b-bed0-e8025f6d3463",
          "8c6ec26471b8a2eb21de140e601445e8df750f0d89a744b736154a85b1a35eaa");
      var config = new AmazonS3Config { ServiceURL = "https://spotify-mahdavi.s3.ir-thr-at1.arvanstorage.ir" };
      IAmazonS3 _s3Client = new AmazonS3Client(awsCredentials, config);
      await ReadObjectDataAsync(_s3Client, bucketName, keyName);
  }
  
  static async Task ReadObjectDataAsync(IAmazonS3 client, string bucketName, string keyName)
  {
      string responseBody = string.Empty;

      try
      {
          GetObjectRequest request = new GetObjectRequest
          {
              BucketName = bucketName,
              Key = keyName,
          };

          using (GetObjectResponse response = await client.GetObjectAsync(request))
          using (Stream responseStream = response.ResponseStream)
          using (StreamReader reader = new StreamReader(responseStream))
          {
              // Assume you have "title" as medata added to the object.
              string title = response.Metadata["x-amz-meta-title"];
              string contentType = response.Headers["Content-Type"];

              Console.WriteLine($"Object metadata, Title: {title}");
              Console.WriteLine($"Content type: {contentType}");

              // Retrieve the contents of the file.
              responseBody = reader.ReadToEnd();

              // Write the contents of the file to disk.
              string filePath = keyName;

              Console.WriteLine("File successfully downloaded");
          }
      }
      catch (AmazonS3Exception e)
      {
          // If the bucket or the object do not exist
          Console.WriteLine($"Error: '{e.Message}'");
      }
  }
  
}



