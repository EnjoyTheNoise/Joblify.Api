using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Data.UnitOfWork;
using Joblify.Core.Login.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Joblify.Core.Login
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoginService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CheckIfUserExists(string email)
        {
            var userExists = await _unitOfWork.UserRepository.Entities.AnyAsync(u => u.Email == email);
            return userExists;
        }

        public async Task<EditProfileDto> SaveProfile(EditProfileDto editProfileDto)
        {
            var user = _unitOfWork.UserRepository.Entities.SingleOrDefault(u => u.Email == editProfileDto.Email);

            if (user == null)
                return await RegisterUser(editProfileDto);

            return await EditProfile(editProfileDto, user);
        }

        public async Task<User> GetUser(string email)
        {
            var user = await _unitOfWork.UserRepository.Entities.SingleOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task DeleteUser(User user)
        {
            user.IsDeleted = true;
            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "\nDeleting failed on save");
                throw;
            }
        }

        private async Task<EditProfileDto> EditProfile(EditProfileDto editProfileDto, User user)
        {
            user = _mapper.Map(editProfileDto, user);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<User, EditProfileDto>(user);
        }

        private async Task<EditProfileDto> RegisterUser(EditProfileDto editProfileDto)
        {
            var userEntity = _mapper.Map<EditProfileDto, User>(editProfileDto);

            var role = await _unitOfWork.RoleRepository.Entities.SingleAsync(r => r.Name == editProfileDto.RoleName);
            if (role == null)
            {
                return null;
            }

            var externalProvider = await _unitOfWork.ExternalProviderRepository.Entities.SingleAsync(e => e.Name == editProfileDto.ExternalProviderName);
            if (externalProvider == null)
            {
                return null;
            }

            userEntity.Role = role;
            userEntity.ExternalProvider = externalProvider;

            await _unitOfWork.UserRepository.AddAsync(userEntity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<User, EditProfileDto>(userEntity);
        }

    }
}
