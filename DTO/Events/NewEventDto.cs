
namespace Volunteer_API.DTO.Events
{
    public class NewEventDto
    {
        public string Month { get; set; } = string.Empty;
        public string Day { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string skillLevel { get; set; } = string.Empty;
    }
}