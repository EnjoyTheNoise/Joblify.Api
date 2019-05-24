using System.Threading.Tasks;
using Joblify.Core.Data.Models;
using Joblify.Core.Users.Dto;

namespace Joblify.Core.Users
{
    public interface IUserService
    {
        Task<UserDto> GetUser(string email);
        Task<AddUserDto> CreateUser(AddUserDto editProfileDto);
        Task<UpdateUserDto> UpdateUser(UpdateUserDto editProfileDto);
        Task DeleteUser(string email);
        Task<bool> CheckIfUserExists(string email);
    }
}
