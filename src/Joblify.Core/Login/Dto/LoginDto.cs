using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Joblify.Core.Login.Dto
{
    public class LoginDto
    {
        [Required]
        public string ExternalProviderToken { get; set; }

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
