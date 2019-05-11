using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Offers;
using Joblify.Core.User.Dto;

namespace Joblify.Core.Api.Infrastructure
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Data.Models.User, EditProfileDto>();
            CreateMap<EditProfileDto, Data.Models.User>();
            CreateMap<Offer, OfferDto>();
        }
    }
}
 