using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Joblify.Core.Data.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Url { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
