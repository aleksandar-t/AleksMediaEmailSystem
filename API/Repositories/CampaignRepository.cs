using AleksMediaEmailSystem.API.Models;
using AleksMediaEmailSystem.Database;

namespace AleksMediaEmailSystem.API.Repositories
{
    public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Add any campaign-specific methods here
    }
}
