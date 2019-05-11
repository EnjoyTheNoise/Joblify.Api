using System.Threading.Tasks;
using Joblify.Core.Data.Models;
using Joblify.Core.Login.Dto;

namespace Joblify.Core.Login
{
    public interface ILoginService
    {
        Task<bool> CheckIfUserExists(string email);
        Task<EditProfileDto> SaveProfile(EditProfileDto editProfileDto);
        Task<User> GetUser(string email);
        Task DeleteUser(User user);
    }
}
