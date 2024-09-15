using AleksMediaEmailSystem.API.Models;

namespace AleksMediaEmailSystem.API.Services
{
    public interface IEmailTemplateService
    {
        Task<EmailTemplate> GetTemplateByIdAsync(int templateId);
        Task<List<EmailTemplate>> GetAllTemplatesAsync();
        Task AddTemplateAsync(EmailTemplate template);
        Task UpdateTemplateAsync(EmailTemplate template);
        Task DeleteTemplateAsync(int templateId);
    }
}
