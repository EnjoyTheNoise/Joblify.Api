using System.Threading.Tasks;
using Joblify.Core.Api.Infrastructure.ActionFilterAttributes;
using Joblify.Core.Offers;
using Joblify.Search;
using Microsoft.AspNetCore.Mvc;


namespace Joblify.Core.Api.Offers
{
    [Route("api/offer")]
    public class OfferController : Controller
    {
        private readonly IOfferSearchIndex _offerSearchIndex;

        public OfferController(IOfferSearchIndex offerSearchIndex)
        {
            _offerSearchIndex = offerSearchIndex;
        }

        [HttpPost]
        [ValidateModel]
        public IActionResult AddOffer([FromBody] OfferDto offerDto)
        {
            var result =  _offerSearchIndex.AddOfferAsync(offerDto);

            if(result == null)
            {
                return BadRequest();
            }
            return Created("", result);
        }
    }
}
