﻿using Joblify.Search.Models;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace Joblify.Search
{
    public class OfferSearchIndex : SearchIndexClient, IOfferSearchIndex
    {
        private readonly SearchServiceClient _searchService;
        private readonly string _indexName;

        public OfferSearchIndex(IConfiguration configuration)
            : base(configuration["SearchServiceName"], configuration["OfferSearchIndexName"],
                  new SearchCredentials(configuration["SearchServiceQueryApiKey"]))
        {
            _searchService = new SearchServiceClient(configuration["SearchServiceName"],
              new SearchCredentials(configuration["SearchServiceAdminApiKey"]));
            _indexName = configuration["OfferSearchIndexName"];
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

        private async Task<bool> AddBatchToIndexAsync(IndexBatch<OfferSearchModel> offerBatch, int count = 0)
        {
            try
            {
                var result = await Documents.IndexAsync(offerBatch);
                return result.Results[0].Succeeded;
            }
            catch (IndexBatchException e)
            {   // sometimes adding to index may fail 
                // (may happen if service is under heavy load)
                
                if (count < 5 && e.Response.StatusCode != HttpStatusCode.NotFound
                    && e.Response.StatusCode != HttpStatusCode.BadRequest)
                {
                    Thread.Sleep(5000);
                    await AddBatchToIndexAsync(offerBatch, ++count);
                }
                return false;
            }
        }

        public async Task<OfferSearchModel> AddOfferAsync(OfferSearchModel offer)
        {
            if (!_searchService.Indexes.Exists(_indexName))
            {
                CreateIndex();
            }
            
            var action = new IndexAction<OfferSearchModel>[] { IndexAction.Upload(offer) };
            var batch = IndexBatch.New(action);

            await AddBatchToIndexAsync(batch);
            return offer;
        }

        public OfferModelWithPageCount SearchOffers(SearchParameters parameters, string phrase="*", int offersInPage=5)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                phrase = "*";
            
            var searchResult = Documents.Search<OfferSearchModel>(phrase, parameters);
            parameters.Skip = null;
            parameters.Top = null;
            var searchToCount = Documents.Search<OfferSearchModel>(phrase, parameters);

            OfferModelWithPageCount result = new OfferModelWithPageCount()
            {
                FoundOffers = searchResult.Results,
                PagesCount = searchToCount.Results.Count / offersInPage
            };

            return result;
        }
    }
}
