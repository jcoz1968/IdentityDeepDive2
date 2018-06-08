using System.ComponentModel.DataAnnotations;

namespace IdentityDeepDive.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}