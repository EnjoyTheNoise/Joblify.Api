using Joblify.Search.Models;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Joblify.Search
{
    public class OfferModelWithPageCount
    {
        public IList<SearchResult<OfferSearchModel>> FoundOffers { get; set; }

        public int OffersCount { get; set; }
    }
}
