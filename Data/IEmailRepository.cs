using Volunteer_API.DTO.Email;

namespace Volunteer_API.Data
{
    public interface IEmailRepository
    {
        void sendEmail(EmailDTO request);
    }
}