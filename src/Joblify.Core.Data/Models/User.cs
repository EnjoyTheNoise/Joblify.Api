using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Joblify.Core.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ExternalProviderToken { get; set; }

        [Required]
        [ForeignKey("ExternalProvider")]
        public byte ExternalProviderId { get; set; }

        public ExternalProvider ExternalProvider { get; set; }  

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }

        public string Experiance { get; set; }

        public string FieldOfInterest { get; set; }

        public string FilesUrl { get; set; }

        [Required]
        [ForeignKey("Role")]
        public byte RoleId { get; set; }

        public Role Role { get; set; }

        public bool IsDeleted { get; set; }


    }
}
