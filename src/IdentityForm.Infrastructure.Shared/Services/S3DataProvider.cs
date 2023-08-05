using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using IdentityForm.Infrastructure.Shared.Configurations;
using IdentityForm.Infrastructure.Shared.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityForm.Infrastructure.Shared.Services
{
    public class S3DataProvider : IS3DataProvider
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly AwsConfiguration _awsConfiguration;
        private readonly ILogger<S3DataProvider> _logger;

        public S3DataProvider(IOptions<AwsConfiguration> awsSettings, ILogger<S3DataProvider> logger)
        {
            _awsConfiguration = awsSettings.Value;
            var s3Credentials = new BasicAWSCredentials(awsSettings.Value.KeyId, awsSettings.Value.SecretKey);
            _amazonS3 = new AmazonS3Client(s3Credentials, new AmazonS3Config
            {
                ServiceURL = "https://localhost:5003",
                UseHttp = true,
                ForcePathStyle = true,
                RegionEndpoint = RegionEndpoint.USEast1
            });
            _logger = logger;
        }

        public async Task DeteteObjectAsync(string objectKey, bool publicBucket = false, CancellationToken cancellationToken = default)
        {
            try
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = publicBucket ? _awsConfiguration.BucketNamePublic : _awsConfiguration.BucketName,
                    Key = objectKey
                };

                await _amazonS3.DeleteObjectAsync(deleteObjectRequest);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when deleting an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when deleting an object", e.Message);
            }
            return;
        }

        public async Task<Stream> GetStreamAsync(string objectKey, bool publicBucket = false, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = publicBucket ? _awsConfiguration.BucketNamePublic : _awsConfiguration.BucketName,
                    Key = objectKey
                };

                using var response = await _amazonS3.GetObjectAsync(request, cancellationToken);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    var stream = new MemoryStream();

                    await using var responseStream = response.ResponseStream;

                    responseStream.CopyTo(stream);

                    stream.Position = 0;

                    return stream;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occurred while reading the S3 object into the memory stream.");
            }

            return Stream.Null;
        }

        public async Task<Stream> PostStreamAsync(string filePath, Stream stream, bool publicBucket = false, CancellationToken cancellationToken = default)
        {
            try
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = publicBucket ? _awsConfiguration.BucketNamePublic : _awsConfiguration.BucketName,
                    Key = filePath,
                    ContentType = "text/plain"
                };
                putRequest.InputStream = stream;
                putRequest.Metadata.Add("x-amz-meta-title", "UploadFile");
                PutObjectResponse response = await _amazonS3.PutObjectAsync(putRequest);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occurred while reading the S3 object into the memory stream.");
            }

            return Stream.Null;
        }
    }
}
