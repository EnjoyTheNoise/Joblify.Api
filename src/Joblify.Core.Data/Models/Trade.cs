using System.ComponentModel.DataAnnotations;

namespace Joblify.Core.Data.Models
{
    public class Trade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
