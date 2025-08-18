using Microsoft.AspNetCore.Http;

namespace Forum.Infrastructure.Extensions
{
    public static class FileExtensions
    {
        public static byte[]? GetBytes(this IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    formFile.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            return null;
        }
    }
}
