using AleksMediaEmailSystem.API.Models;

namespace AleksMediaEmailSystem.API.Services
{
    public interface ICampaignService
    {
        Task<Campaign> GetCampaignByIdAsync(int campaignId);
        Task<List<Campaign>> GetAllCampaignsAsync();
        Task AddCampaignAsync(Campaign campaign);
        Task UpdateCampaignAsync(Campaign campaign);
        Task DeleteCampaignAsync(int campaignId);
        Task TriggerCampaignEmailsAsync(int campaignId);
        Task TriggerCampaignFromXmlFileAsync(string xmlFilePath);
    }
}
