using System.ComponentModel.DataAnnotations;

namespace Forum.Web.Models
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }
    }
}
