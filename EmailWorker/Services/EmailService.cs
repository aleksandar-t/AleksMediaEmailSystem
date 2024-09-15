
using AleksMediaEmailSystem.EmailWorker.Models;
using System.Net.Mail;

namespace AleksMediaEmailSystem.EmailWorker.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            // Example using SMTP
            using var smtpClient = new SmtpClient("smtp.example.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential("user@example.com", "password"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("noreply@example.com"),
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(message.EmailAddress);

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendEmailDemoAsync(EmailMessage message) 
        {
            await Task.Delay(2000);
        }
    }
}
