namespace Volunteer_API.DTO.Users
{
    public class UserRegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string skillLevel { get; set; } = string.Empty;
    }
}