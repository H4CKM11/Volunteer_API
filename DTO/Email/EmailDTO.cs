
namespace Volunteer_API.DTO.Email
{
    public class EmailDTO
    {
        public int Id { get; set; }
        public string email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}