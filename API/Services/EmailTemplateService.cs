using AleksMediaEmailSystem.API.Models;
using AleksMediaEmailSystem.API.Repositories;

namespace AleksMediaEmailSystem.API.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;

        public EmailTemplateService(IEmailTemplateRepository emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }

        public async Task<EmailTemplate> GetTemplateByIdAsync(int templateId)
        {
            return await _emailTemplateRepository.GetByIdAsync(templateId);
        }

        public async Task<List<EmailTemplate>> GetAllTemplatesAsync()
        {
            return await _emailTemplateRepository.GetAllAsync();
        }

        public async Task AddTemplateAsync(EmailTemplate template)
        {
            await _emailTemplateRepository.AddAsync(template);
        }

        public async Task UpdateTemplateAsync(EmailTemplate template)
        {
            await _emailTemplateRepository.UpdateAsync(template);
        }

        public async Task DeleteTemplateAsync(int templateId)
        {
            var template = await _emailTemplateRepository.GetByIdAsync(templateId);
            if (template != null)
            {
                await _emailTemplateRepository.DeleteAsync(template);
            }
        }
    }
}
