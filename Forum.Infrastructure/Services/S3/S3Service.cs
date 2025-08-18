using Amazon.S3;
using Amazon.S3.Model;
using Forum.Infrastructure.Services.S3.Models.Requests;
using Microsoft.Extensions.Logging;

namespace Forum.Infrastructure.Services.S3
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly ILogger<S3Service> _logger;

        public S3Service(IAmazonS3 amazonS3, ILogger<S3Service> logger)
        {
            _amazonS3 = amazonS3;
            _logger = logger;
        }

        public async Task DeleteFile(string path, string bucketName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            var request = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = path,
            };

            try
            {
                await _amazonS3.DeleteObjectAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (AmazonS3Exception ex)
            {
                _logger.LogError(ex, $"File: {path} failed to delete");
            }
        }

        public async Task<string?> UploadFile(FileUploadRequest request, string bucket, CancellationToken cancellationToken)
        {
            string fileName = $"{request.FileName ?? Guid.NewGuid().ToString()}{request.Extension}";
            string key = $"{request.Folder.Trim('/', '\\')}/{fileName}";

            var putRequest = new PutObjectRequest
            {
                BucketName = bucket,
                ContentType = request.ContentType,
                Key = key,
                InputStream = new MemoryStream(request.Content),
                AutoCloseStream = true
            };

            try
            {
                await _amazonS3.PutObjectAsync(putRequest, cancellationToken).ConfigureAwait(false);
                return fileName;
            }
            catch (AmazonS3Exception ex)
            {
                _logger.LogError(ex, $"File: {fileName} failed to upload");
                return null;
            }
        }
    }
}
