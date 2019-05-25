using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Offers;
using Joblify.Core.Users.Dto;

namespace Joblify.Core.Api.Infrastructure
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, AddUserDto>();
            CreateMap<User, UpdateUserDto>();
            CreateMap<User, UserDto>();
            CreateMap<AddUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<Offer, OfferDto>();
        }
    }
}
 