using System.ComponentModel.DataAnnotations;

namespace LMS_WEBSITE.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string? Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}