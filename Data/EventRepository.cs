using Microsoft.EntityFrameworkCore;
using Volunteer_API.DTO.Events;
using Volunteer_API.Model;

namespace Volunteer_API.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;

        public EventRepository(DataContext context, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.context = context;
        }

        public async Task<ServiceResponse<int>> NewEvent(Event newEvent, string description, string month, string day)
        {
            var response = new ServiceResponse<int>();

            newEvent.Description = description;
            newEvent.Month = month;
            newEvent.Day = day;
            this.context.Events.Add(newEvent);
            await this.context.SaveChangesAsync();
            response.Success = true;
            response.Message = "Event Created";
            return response;
        }

        public async Task<ServiceResponseList<List<RetrieveEvent>>> GetEvents()
        {
            var ServiceResponse = new ServiceResponseList<List<RetrieveEvent>>();
            var dbEvents = await this.context.Events.ToListAsync();
            ServiceResponse.events= dbEvents;
            return ServiceResponse;
        }

    }
}