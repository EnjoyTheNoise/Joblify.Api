using System.Collections;
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
        Task<Offer> GetOfferEntity(int id);
        Task<IEnumerable<EditOfferDto>> GetOffersForUser(int id);
        Task<bool> CheckIfOfferExist(int id);
        Task <bool> UpdateOffer(EditOfferDto offer,int id);
    }
}