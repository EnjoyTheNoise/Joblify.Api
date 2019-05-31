﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Joblify.Core.Data.Models;
using Joblify.Core.Offers.Dto;

namespace Joblify.Core.Offers
{
    public interface IOfferService
    {
        Task<OfferDto> AddOfferAsync(OfferDto offerDto);
        Task<IEnumerable<GetAllCategoriesDto>> GetAllCategories();
        Task<IEnumerable<GetAllTradesDto>> GetAllTrades();
        Task<GetOfferByIdDto> GetOfferById(int id);
        Task<IEnumerable<OfferDto>> GetOffersForUser(int id);
    }
}