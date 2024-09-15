using AleksMediaEmailSystem.API.Models;
using AleksMediaEmailSystem.Database;

namespace AleksMediaEmailSystem.API.Repositories
{
    public class EmailTemplateRepository : GenericRepository<EmailTemplate>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Add any email template-specific methods here
    }
}
