using Forum.Infrastructure.Services.S3.Models.Requests;

namespace Forum.Infrastructure.Services.S3
{
    public interface IS3Service
    {
        Task<string?> UploadFile(FileUploadRequest request, string bucket, CancellationToken cancellationToken);
        Task DeleteFile(string path, string bucketName, CancellationToken cancellationToken);
    }
}
