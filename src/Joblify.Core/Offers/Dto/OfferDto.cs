using System.ComponentModel.DataAnnotations;

namespace Joblify.Core.Offers.Dto
{
    public class OfferDto
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Trade { get; set; }

        public string AvailableTime { get; set; }

        [Required]
        public int UserId { get; set;}

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
