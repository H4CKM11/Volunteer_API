using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Volunteer_API.DTO.Users;
using Volunteer_API.Model;

namespace Volunteer_API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.context = context;

        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            if(user is null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if(!VerifyPassWordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong Password";
            }
            else
            {
                response.Success = true;
                response.Data = CreateToken(user);
            }

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password, string _email, string _skillLevel)
        {
            var response = new ServiceResponse<int>();
            if(await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "User Already Exist.";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Email = _email;
            user.skillLevel = _skillLevel;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
            response.Data = user.Id;
            response.Success = true;
            response.Message = "User Created";
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await this.context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPassWordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return ComputeHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var appSettingToken = this.configuration.GetSection("AppSettings:Token").Value;
            if(appSettingToken is null)
            {
                throw new Exception("App Token is NULL");
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingToken));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<ServiceResponseListUser<List<GetAllUsersDTO>>> getUsers()
        {
            var ServiceResponse = new ServiceResponseListUser<List<GetAllUsersDTO>>();
            var dbUsers = await this.context.Users.ToListAsync();
            ServiceResponse.users = dbUsers;

            return ServiceResponse;
        }

        public async Task<ServiceResponseListUser<List<GetSkillUserDTO>>> getSkilledUsers(string skill)
        {
            var response = new ServiceResponseListUser<List<GetSkillUserDTO>>();
            var dbUsers = await this.context.Users.Where(e => e.skillLevel.Contains(skill)).ToListAsync();
            response.users = dbUsers;
            return response;
        }
    }
}