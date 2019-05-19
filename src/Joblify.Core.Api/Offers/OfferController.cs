using System.Threading.Tasks;
using Joblify.Core.Api.Infrastructure.ActionFilterAttributes;
using Joblify.Core.Offers;
using Joblify.Core.Offers.Dto;
using Microsoft.AspNetCore.Mvc;


namespace Joblify.Core.Api.Offers
{
    [Route("api/offer")]
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
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

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _offerService.GetOfferById(id);

            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
