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
        public async Task<IActionResult> AddOfferAsync([FromBody] OfferDto offerDto)
        {
            var result = await _offerSearchIndex.AddOfferAsync(offerDto);

            if(result == null)
            {
                return BadRequest();
            }
            return Created("", result);
        }

        [HttpGet("search/{pattern}")]
        public IActionResult SearchByString(string pattern)
        {
            var result = _offerSearchIndex.SearchOffersByString(pattern);

            return Ok(result);
        }
    }
}
