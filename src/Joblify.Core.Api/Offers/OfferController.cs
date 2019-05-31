using System.Collections.Generic;
using System.Threading.Tasks;
using Joblify.Core.Api.Infrastructure.ActionFilterAttributes;
using Joblify.Core.Offers;
using Joblify.Core.Offers.Dto;
using Joblify.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Search.Models;

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

        [HttpGet("search/page/{page:int}")]
        public IActionResult GetAllEmployees(string trade, string category, string orderby,[FromQuery] int page=1, int offersInPage=5, string phrase="*")
        {

            var parameters = new SearchParameters();

            if(orderby != null)
            {
                var orderList = new List<string>();
                orderList.Add(orderby);
                parameters.OrderBy = orderList;
            }

            if (category != null)
                parameters.Filter = $"category eq '{category}'";

            if (trade != null && orderby != "All")
            {
                if (category != null)
                    parameters.Filter += $" and trade eq '{trade}'";
                else
                    parameters.Filter = $"trade eq '{trade}'";
            }

            parameters.Skip = (page-1)*offersInPage;
            parameters.Top = offersInPage;

            var result = _offerSearchIndex.SearchOffers(parameters, phrase, offersInPage);
            return Ok(result);
        }
    }
}
