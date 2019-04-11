using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Joblify.Core.Login.Dto;

namespace Joblify.Core.Login
{
    public interface ILoginService
    {
        Task<bool> CheckIfUserExists(LoginDto loginDto);
        Task RegisterUser(LoginDto loginDto);
    }
}
