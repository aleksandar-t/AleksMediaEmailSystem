using AleksMediaEmailSystem.EmailWorker.Models;

namespace AleksMediaEmailSystem.EmailWorker.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
        Task SendEmailDemoAsync(EmailMessage message);
    }
}
