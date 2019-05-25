using System.Threading.Tasks;
using Joblify.Core.Api.Infrastructure.ActionFilterAttributes;
using Joblify.Core.Offers;
using Joblify.Core.Offers.Dto;
using Joblify.Search;
using Microsoft.AspNetCore.Mvc;


namespace Joblify.Core.Api.Offers
{
    [Route("api/offer")]
    public class OfferController : Controller
    {
        private readonly IOfferSearchIndex _offerSearchIndex;
        private readonly IOfferService _offerService;

        public OfferController(IOfferSearchIndex offerSearchIndex, IOfferService offerService)
        {
            _offerSearchIndex = offerSearchIndex;
            _offerService = offerService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddOfferAsync([FromBody] OfferDto offerDto)
        {
            var result = await _offerService.AddOfferAsync(offerDto);

            if(result == null)
            {
                return BadRequest();
            }

            return Created("", result);
        }

        [HttpGet("getAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _offerService.GetAllCategories();

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        [HttpGet("getAllTrades")]
        public async Task<IActionResult> GetAllTrades()
        {
            var response = await _offerService.GetAllTrades();

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
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
