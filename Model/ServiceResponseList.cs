namespace Volunteer_API.Model
{
    public class ServiceResponseList<T>
    {
        public List<Event>? list { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}