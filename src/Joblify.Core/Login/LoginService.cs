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

        public async Task<bool> CheckIfUserExists(string email)
        {
            var userExists = await _unitOfWork.UserRepository.Entities.AnyAsync(u => u.Email == email);
            return userExists;
        }

        public async Task<RegisterDto> RegisterUser(RegisterDto loginDto)
        {
            var userEntity = _mapper.Map<RegisterDto, User>(loginDto);

            var role = await _unitOfWork.RoleRepository.Entities.SingleAsync(r => r.Name == loginDto.RoleName);
            if (role == null)
            {
                return null;
            }

            var externalProvider = await _unitOfWork.ExternalProviderRepository.Entities.SingleAsync(e => e.Name == loginDto.ExternalProviderName);
            if (externalProvider == null)
            {
                return null;
            }

            userEntity.Role = role;
            userEntity.ExternalProvider = externalProvider;

            await _unitOfWork.UserRepository.AddAsync(userEntity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<User, RegisterDto>(userEntity);
        }
    }
}
