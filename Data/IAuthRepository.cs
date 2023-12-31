using Volunteer_API.DTO.Users;
using Volunteer_API.Model;


namespace Volunteer_API.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password, string email, string _skillLevel);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<ServiceResponseListUser<List<GetAllUsersDTO>>> getUsers();
        Task<ServiceResponseListUser<List<GetSkillUserDTO>>> getSkilledUsers(string skill);
    }
}