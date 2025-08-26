using System.ComponentModel.DataAnnotations;

namespace Forum.Web.Models
{
    public class ValidateOtpViewModel
    {
        [Required]
        public string Otp {  get; set; }
    }
}
