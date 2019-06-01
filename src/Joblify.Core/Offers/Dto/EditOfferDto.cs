using System.ComponentModel.DataAnnotations;

namespace Joblify.Core.Offers.Dto
{
    public class EditOfferDto
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
        
    }
}
