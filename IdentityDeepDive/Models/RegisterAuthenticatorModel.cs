using System.ComponentModel.DataAnnotations;

namespace IdentityDeepDive.Models
{
    public class RegisterAuthenticatorModel
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string AuthenticatorKey { get; set; }
    }
}