using Microsoft.AspNetCore.Mvc;
using Volunteer_API.Data;
using Volunteer_API.DTO.Events;
using Volunteer_API.Model;

namespace Volunteer_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository eventRepos;
        public EventController(IEventRepository eventRepos)
        {
            this.eventRepos = eventRepos;
        }

        [HttpPost("NewEvent")]
        public async Task<ActionResult<ServiceResponse<int>>> NewEvent(NewEventDto request)
        {
            var response = await this.eventRepos.NewEvent(
                new Event {Name = request.Name}, request.Description, request.Month, request.Day
            );
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetEvents")]
        public async Task<ActionResult<ServiceResponse<int>>> GetEvent()
        {
            var response = await this.eventRepos.GetEvents();
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        
    }
}