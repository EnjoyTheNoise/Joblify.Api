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
            CreateMap<OfferDto, Offer>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Trade, opt => opt.Ignore());
            
            CreateMap<Offer, OfferDto>()
                .ForMember(dest => dest.Trade, s => s.MapFrom(src => src.Trade.Name))
                .ForMember(dest => dest.Category, s => s.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.FirstName, s => s.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, s => s.MapFrom(src => src.User.LastName));
            CreateMap<OfferDto, OfferSearchModel>();
            CreateMap<OfferSearchModel, OfferDto>();
        }
    }
}
 