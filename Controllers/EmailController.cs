using Microsoft.AspNetCore.Mvc;
using Volunteer_API.Data;
using Volunteer_API.DTO.Email;

namespace Volunteer_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository emailService;
        public EmailController(IEmailRepository emailService)
        {
            this.emailService = emailService;
        }
        
        [HttpPost]
        public IActionResult SendEmail(EmailDTO request)
        {
            emailService.sendEmail(request);
            return Ok();
        }
    }
}