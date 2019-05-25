using System.Collections.Generic;
using System.Threading.Tasks;
using Joblify.Search.Models;
using Microsoft.Azure.Search.Models;

namespace Joblify.Search
{
    public interface IOfferSearchIndex
    {
        Task<OfferSearchModel> AddOfferAsync(OfferSearchModel offerDto);
        IList<SearchResult<OfferSearchModel>> SearchOffersByString(string searchString);

    }
}