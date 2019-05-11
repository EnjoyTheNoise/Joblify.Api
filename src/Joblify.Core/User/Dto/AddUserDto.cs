using System;
using System.ComponentModel.DataAnnotations;

namespace Joblify.Core.User.Dto
{
    public class AddUserDto
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

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Experience { get; set; }

        [Required]
        public string FieldOfInterest { get; set; }
        
    }
}
