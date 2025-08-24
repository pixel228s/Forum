using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    [Route("S3")]
    public class ImageController : Controller
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;

        public ImageController(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _configuration = configuration;
        }

        [HttpGet("GetImage")]
        public IActionResult GetImage(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest("Missing key");
            }
                
            var bucketName = _configuration["AWS:BucketName"];
            var folder = _configuration["AWS:Folder"];

            var newKey = $"{folder}/{key}";

            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = newKey,
                Expires = DateTime.UtcNow.AddMinutes(10),
                Verb = HttpVerb.GET
            };

            string url = _s3Client.GetPreSignedURL(request);

            return Redirect(url);
        }
    }
}

