using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joblify.Core.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("ExternalProvider")]
        public int ExternalProviderId { get; set; }

        public ExternalProvider ExternalProvider { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }

        public string Experience { get; set; }

        public string FieldOfInterest { get; set; }

        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public bool IsDeleted { get; set; }
    }
}
