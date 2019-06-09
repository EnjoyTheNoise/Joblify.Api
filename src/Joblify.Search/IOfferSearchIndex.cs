using System.Collections.Generic;
using System.Threading.Tasks;
using Joblify.Search.Models;
using Microsoft.Azure.Search.Models;

namespace Joblify.Search
{
    public interface IOfferSearchIndex
    {
        Task<OfferSearchModel> AddOfferAsync(OfferSearchModel offerDto);

        OfferModelWithPageCount SearchOffers(SearchParameters parameters, string phrase, int offersInPage);
    }
}