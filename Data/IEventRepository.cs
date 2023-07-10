using Volunteer_API.Model;

namespace Volunteer_API.Data
{
    public interface IEventRepository
    {
        Task<ServiceResponse<int>> NewEvent(Event newEvent, string description, string month, string day);
    }
}