﻿using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Login.Dto;
using Joblify.Core.Offers;


namespace Joblify.Core.Api.Infrastructure
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, EditProfileDto>();
            CreateMap<EditProfileDto, User>();
            CreateMap<Offer, OfferDto>();
        }
    }
}
 