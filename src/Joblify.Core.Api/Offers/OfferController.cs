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

        [HttpGet("search/employees")]
        public IActionResult GetAllEmployees(string pattern)
        {
            var result = _offerSearchIndex.SearchOffersByString("category eq 'employee'");

            return Ok(result);
        }

        [HttpGet("search/employers")]
        public IActionResult GetAllEmployers(string pattern)
        {
            var result = _offerSearchIndex.SearchOffersByString("category eq 'employer'");

            return Ok(result);
        }

        [HttpGet("search/employee/{pattern}")]
        public IActionResult SearchByStringEmployee(string pattern)
        {
            var result = _offerSearchIndex.SearchOffersByString("category eq 'employee' and " + pattern);

            return Ok(result);
        }

        [HttpGet("search/employer/{pattern}")]
        public IActionResult SearchByStringEmployer(string pattern)
        {
            var result = _offerSearchIndex.SearchOffersByString("category eq 'employer' and " + pattern);

            return Ok(result);
        }
    }
}
