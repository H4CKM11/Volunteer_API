using MailKit.Net.Smtp;
using MimeKit;
using Volunteer_API.DTO.Email;

namespace Volunteer_API.Data
{
    public class EmailRepository : IEmailRepository
    {
        public IConfiguration Config { get; }
        public DataContext DbContext { get; }
        public EmailRepository(IConfiguration config, DataContext dbContext)
        {
            this.DbContext = dbContext;
            this.Config = config;

        }
        public void sendEmail(EmailDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(Config.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(request.email));
            email.Subject = "Invitation! Join Us And Make A Difference Now";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(Config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(Config.GetSection("EmailUserName").Value, Config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void sendEmailToAllUsers()
        {
            var users = this.DbContext.Users.ToList();
            var eventLists = this.DbContext.Events.ToList();
            foreach(var user in users)
            {
                var emailRequest = new EmailDTO
                {
                    email = user.Email,
                    Subject = "Dont Forget To Sign Up!"
                };
                foreach (var eventList in eventLists)
                {
                    emailRequest.Body = eventList.Name;
                }
                
                sendEmail(emailRequest);
            }
        }
    }
}