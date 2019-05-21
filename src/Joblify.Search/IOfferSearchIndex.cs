using System.Collections.Generic;
using System.Threading.Tasks;
using Joblify.Core.Offers;
using Joblify.Search.Models;
using Microsoft.Azure.Search.Models;

namespace Joblify.Search
{
    public interface IOfferSearchIndex
    {
        Task<OfferDto> AddOfferAsync(OfferDto offerDto);
        IList<SearchResult<OfferSearchModel>> SearchOffersByString(string searchString);
    }
}