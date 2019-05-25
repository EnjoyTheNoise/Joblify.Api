using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Offers;
using Joblify.Search.Models;
using Joblify.Core.Users.Dto;

namespace Joblify.Core.Api.Infrastructure
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OfferDto, Offer>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Trade, opt => opt.Ignore());
            CreateMap<Offer, OfferSearchModel>()
                .ForMember(dest => dest.Id, s => s.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Trade, s => s.MapFrom(src => src.Trade.Name))
                .ForMember(dest => dest.Category, s => s.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.FirstName, s => s.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, s => s.MapFrom(src => src.User.LastName));
            CreateMap<User, AddUserDto>();
            CreateMap<User, UpdateUserDto>();
            CreateMap<User, UserDto>();
            CreateMap<AddUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<Offer, OfferDto>();
        }
    }
}
 