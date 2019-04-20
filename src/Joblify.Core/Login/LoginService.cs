using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Data.UnitOfWork;
using Joblify.Core.Login.Dto;
using Microsoft.EntityFrameworkCore;

namespace Joblify.Core.Login
{
    public class LoginService: ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoginService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CheckIfUserExists(LoginDto loginDto)
        {
            var user = await _unitOfWork.UserRepository.Entities
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email || u.ExternalProviderToken == loginDto.ExternalProviderToken);
            if (user is null)
            {
                return false;
            }

            return true;
        }

        public async Task RegisterUser(LoginDto loginDto)
        {
            var userEntity = _mapper.Map<LoginDto, User>(loginDto);
            var role = await _unitOfWork.RoleRepository.Entities.FirstOrDefaultAsync(r => r.Name == loginDto.RoleName);
            var externalProvider = await _unitOfWork.ExternalProviderRepository.Entities.FirstOrDefaultAsync(e => e.Name == loginDto.ExternalProviderName);

            userEntity.Role = role;
            userEntity.ExternalProvider = externalProvider;

            await _unitOfWork.UserRepository.AddAsync(userEntity);
            _unitOfWork.Commit();
        }
    }
}
