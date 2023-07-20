using Microsoft.AspNetCore.Mvc;
using Volunteer_API.Data;
using Volunteer_API.DTO.Users;
using Volunteer_API.Model;

namespace Volunteer_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            this.authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await this.authRepo.Register(
                new User {Username = request.Username}, request.Password,request.Email, request.skillLevel
            );
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
        {
            var response = await this.authRepo.Login(request.username, request.password);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<ServiceResponse<int>>> getUsers()
        {
            var response = await this.authRepo.getUsers();

            return Ok(response);
        }

        [HttpGet("searchSkilledUsers")]
        public async Task<ActionResult<ServiceResponse<int>>> getSkilledUsers(string skillLevel)
        {
            var response = await this.authRepo.getSkilledUsers(skillLevel);

            return Ok(response);
        }
    }
}