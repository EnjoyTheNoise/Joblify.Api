using System.Threading.Tasks;
using Joblify.Core.User.Dto;

namespace Joblify.Core.User
{
    public interface IUserService
    {
        Task<Data.Models.User> GetUser(string email);
        Task<AddUserDto> CreateUser(AddUserDto editProfileDto);
        Task<AddUserDto> UpdateUser(UpdateUserDto editProfileDto);
        Task DeleteUser(Data.Models.User user);
    }
}
