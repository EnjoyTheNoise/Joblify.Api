using System.Threading.Tasks;
using Joblify.Core.Login.Dto;

namespace Joblify.Core.Login
{
    public interface ILoginService
    {
        Task<bool> CheckIfUserExists(string email);
        Task<EditProfileDto> SaveProfile(EditProfileDto editProfileDto);
    }
}
