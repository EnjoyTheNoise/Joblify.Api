using AutoMapper;
using Joblify.Core.Offers;
using Joblify.Search.Models;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Joblify.Search
{
    public class OfferSearchIndex : SearchIndexClient, IOfferSearchIndex
    {
        private readonly IOfferService _offerService;
        private readonly SearchServiceClient _searchService;
        private readonly IMapper _mapper;
        private readonly string _indexName;

        public OfferSearchIndex(IConfiguration configuration, IMapper mapper, IOfferService offerService)
            : base(configuration["SearchServiceName"], configuration["OfferSearchIndexName"],
                  new SearchCredentials(configuration["SearchServiceQueryApiKey"]))
        {
            _searchService = new SearchServiceClient(configuration["SearchServiceName"],
              new SearchCredentials(configuration["SearchServiceAdminApiKey"]));
            _indexName = configuration["OfferSearchIndexName"];
            _offerService = offerService;
            _mapper = mapper;
        }

        private void CreateIndex()
        {
            var definition = new Index()
            {
                Name = _indexName,
                Fields = FieldBuilder.BuildForType<OfferSearchModel>()
            };

            _searchService.Indexes.Create(definition);
        }

        private bool AddBatchToIndex(IndexBatch<OfferSearchModel> offerBatch, int count = 0)
        {
            try
            {
                Documents.Index(offerBatch);
                return true;
            }
            catch (IndexBatchException)
            {   // sometimes adding to index may fail 
                // (may happen if service is under heavy load)
                if (count < 5)
                {
                    Thread.Sleep(5000);
                    return AddBatchToIndex(offerBatch, ++count);
                }
                return false;
            }
        }

        public async Task<OfferDto> AddOfferAsync(OfferDto offerDto)
        {
            if (!_searchService.Indexes.Exists(_indexName))
            {
                CreateIndex();
            }

            var offer = await _offerService.AddOfferAsync(offerDto);

            var offerModel = _mapper.Map<OfferSearchModel>(offer);
            var action = new IndexAction<OfferSearchModel>[] { IndexAction.Upload(offerModel) };
            var batch = IndexBatch.New(action);

            AddBatchToIndex(batch);
            return offerDto;
        }

        public IList<SearchResult<OfferSearchModel>> SearchOffersByString(string searchString)
        {
            var results = Documents.Search<OfferSearchModel>(searchString);
            return results.Results;
        }
        
    }
}
