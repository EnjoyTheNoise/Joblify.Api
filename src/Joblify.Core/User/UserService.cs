using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Joblify.Core.Data.UnitOfWork;
using Joblify.Core.User.Dto;
using Microsoft.EntityFrameworkCore;

namespace Joblify.Core.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AddUserDto> CreateUser(AddUserDto userDto)
        {
            var role = await _unitOfWork.RoleRepository.Entities.SingleAsync(r => r.Name == userDto.RoleName);
            if (role == null)
            {
                return null;
            }

            var externalProvider = await _unitOfWork.ExternalProviderRepository.Entities.SingleAsync(e => e.Name == userDto.ExternalProviderName);
            if (externalProvider == null)
            {
                return null;
            }

            var userEntity = _mapper.Map<AddUserDto, Data.Models.User>(userDto);
            userEntity.Role = role;
            userEntity.ExternalProvider = externalProvider;

            await _unitOfWork.UserRepository.AddAsync(userEntity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<Data.Models.User, AddUserDto>(userEntity);
        }

        public async Task<AddUserDto> UpdateUser(UpdateUserDto userDto)
        {
            var userEntity = _unitOfWork.UserRepository.Entities.SingleOrDefault(u => u.Email == userDto.Email);
            if (userEntity == null)
            {
                throw new Exception("User does not exist");
            }

            _mapper.Map(userDto, userEntity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<Data.Models.User, AddUserDto>(userEntity);
        }

        public async Task<Data.Models.User> GetUser(string email)
        {
            var user = await _unitOfWork.UserRepository.Entities.SingleOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task DeleteUser(Data.Models.User user)
        {
            user.IsDeleted = true;
            await _unitOfWork.CommitAsync();
        }
    }
}
