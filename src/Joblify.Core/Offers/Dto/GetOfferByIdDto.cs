using System.ComponentModel.DataAnnotations;

namespace Joblify.Core.Offers.Dto
{
    public class GetOfferByIdDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string AvailableTime { get; set; }

        public UserDto User { get; set; }
    }
}