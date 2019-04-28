using System.ComponentModel.DataAnnotations;

namespace Joblify.Core.Login.Dto
{
    public class RegisterDto
    {
        [Required]
        public string ExternalProviderName { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhotoUrl { get; set; }
    }
}
