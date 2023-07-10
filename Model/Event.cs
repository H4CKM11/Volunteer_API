namespace Volunteer_API.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Month { get; set; } = string.Empty;
        public string Day { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Volunteers  { get; set; }
    }
}