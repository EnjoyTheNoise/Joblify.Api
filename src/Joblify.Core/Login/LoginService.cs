using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Joblify.Core.Data.Models;
using Joblify.Core.Data.UnitOfWork;
using Joblify.Core.Login.Dto;

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
        public bool CheckIfUserExists(LoginDto loginDto)
        {
            var user = _unitOfWork.UserRepository.Entities
                .FirstOrDefault(u => u.Email == loginDto.Email || u.ExternalProviderToken == loginDto.ExternalProviderToken);
            if (user is null)
            {
                return false;
            }

            return true;
        }

        public void RegisterUser(LoginDto loginDto)
        {
            var userEntity = _mapper.Map<LoginDto, User>(loginDto);
            _unitOfWork.UserRepository.Add(userEntity);
            _unitOfWork.Commit();
        }
    }
}
