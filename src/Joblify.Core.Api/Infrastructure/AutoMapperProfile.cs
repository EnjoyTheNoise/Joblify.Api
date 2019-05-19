using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Login.Dto;
using Joblify.Core.Offers;
using Joblify.Core.Offers.Dto;


namespace Joblify.Core.Api.Infrastructure
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<Offer, OfferDto>();
            CreateMap<OfferDto, Offer>();
            CreateMap<Category, GetAllCategoriesDto>();
            CreateMap<Trade, GetAllTradesDto>();
            CreateMap<Offer, GetOfferByIdDto>();
            CreateMap<User, UserDto>();
        }
    }
}
 