using Volunteer_API.DTO.Events;
using Volunteer_API.Model;

namespace Volunteer_API.Data
{
    public interface IEventRepository
    {
        Task<ServiceResponse<int>> NewEvent(Event newEvent, string description, string month, string day, string skillLevel, string location);
        Task<ServiceResponseList<List<RetrieveEvent>>> GetEvents();
        Task<ServiceResponseList<List<RetrieveEvent>>> searchEvent(string eventName);
        Task<ServiceResponse<int>> Registered(UpdateEventDto eventName);
    }

}