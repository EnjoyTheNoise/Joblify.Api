using System;
using System.Collections.Generic;
using System.Text;
using Joblify.Core.Login.Dto;

namespace Joblify.Core.Login
{
    public interface ILoginService
    {
        void CheckIfUserExists(LoginDto loginDto);
        void RegisterUser(LoginDto loginDto);
    }
}
