using System.ComponentModel.DataAnnotations;

namespace Forum.Web.Models
{
    public class ForgotPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
