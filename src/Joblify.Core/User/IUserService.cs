using System.Threading.Tasks;
using Joblify.Core.User.Dto;

namespace Joblify.Core.User
{
    public interface IUserService
    {
        Task<bool> CheckIfUserExists(string email);
        Task<EditProfileDto> SaveProfile(EditProfileDto editProfileDto);
        Task<Data.Models.User> GetUser(string email);
        Task DeleteUser(Data.Models.User user);
    }
}
