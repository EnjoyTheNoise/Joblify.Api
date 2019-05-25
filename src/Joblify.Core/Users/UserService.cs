using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Data.UnitOfWork;
using Joblify.Core.Users.Dto;
using Microsoft.EntityFrameworkCore;

namespace Joblify.Core.Users
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

            var userEntity = _mapper.Map<AddUserDto, User>(userDto);
            userEntity.Role = role;
            userEntity.ExternalProvider = externalProvider;

            await _unitOfWork.UserRepository.AddAsync(userEntity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<User, AddUserDto>(userEntity);
        }

        public async Task<UpdateUserDto> UpdateUser(UpdateUserDto userDto)
        {
            var userEntity = await RetrieveUser(userDto.Email);

            if (userEntity == null)
            {
                 return  null;
            }

            _mapper.Map(userDto, userEntity);
            await _unitOfWork.CommitAsync();

            return userDto;
        }

        public async Task<UserDto> GetUser(string email)
        {
            var user = await RetrieveUser(email);
            if (user == null)
            {
                return null;
            }

            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public async Task DeleteUser(string email)
        {
            var user = await RetrieveUser(email);
            user.IsDeleted = true;

            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> CheckIfUserExists(string email)
        {
            var userExists = await _unitOfWork.UserRepository.Entities.AnyAsync(u => u.Email == email && !u.IsDeleted);
            return userExists;
        }

        private async Task<User> RetrieveUser(string email)
        {
            return await _unitOfWork.UserRepository.Entities.SingleOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        }
    }
}
