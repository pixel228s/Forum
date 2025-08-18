using Forum.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace Forum.Infrastructure.Services.S3.Models.Requests
{
    public class FileUploadRequest
    {
        public string Folder {  get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public string FileName { get; set; }

        public static FileUploadRequest CreateImage(string folder, byte[] file, string extension)
        {
            return new FileUploadRequest
            {
                Folder = folder,
                Content = file,
                Extension = extension,
                ContentType = $"image/{extension.Trim('.')}"
            };
        }

        public static FileUploadRequest CreateImage(string folder, IFormFile file)
        {
            return CreateImage(folder, file.GetBytes()!, Path.GetExtension(file.FileName));
        }
    }
}
