﻿using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Login.Dto;
using Joblify.Core.Offers;
using Joblify.Search.Models;

namespace Joblify.Core.Api.Infrastructure
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<Offer, OfferDto>();
            CreateMap<OfferDto, OfferSearchModel>();
            CreateMap<OfferSearchModel, OfferDto>();
        }
    }
}
 