using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Login.Dto;

namespace Joblify.Core.Api
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, LoginDto>();
            CreateMap<LoginDto, User>();
        }
    }
}
