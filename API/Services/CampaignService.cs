using AleksMediaEmailSystem.API.Models;
using AleksMediaEmailSystem.API.Repositories;
using AleksMediaEmailSystem.EmailWorker.Services;

namespace AleksMediaEmailSystem.API.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IMessageQueueService _rabbitMQPublisher;
        private readonly IMassEmailService _massEmailService;

        public CampaignService(ICampaignRepository campaignRepository, IEmailTemplateRepository emailTemplateRepository, IMessageQueueService rabbitMQPublisher, IMassEmailService massEmailService)
        {
            _campaignRepository = campaignRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _rabbitMQPublisher = rabbitMQPublisher;
            _massEmailService = massEmailService;
        }

        // This method triggers the emails for a campaign from a selected XML file as given in the assignment
        public async Task TriggerCampaignFromXmlFileAsync(string xmlFilePath)
        {
            // Generate and send emails for each client in the campaign (using RabbitMQ for async email sending)
            _massEmailService.ProcessXmlForCampaign(xmlFilePath);
        }

        public async Task<Campaign> GetCampaignByIdAsync(int campaignId)
        {
            return await _campaignRepository.GetByIdAsync(campaignId);
        }

        public async Task<List<Campaign>> GetAllCampaignsAsync()
        {
            return await _campaignRepository.GetAllAsync();
        }

        public async Task AddCampaignAsync(Campaign campaign)
        {
            await _campaignRepository.AddAsync(campaign);
        }

        public async Task UpdateCampaignAsync(Campaign campaign)
        {
            await _campaignRepository.UpdateAsync(campaign);
        }

        public async Task DeleteCampaignAsync(int campaignId)
        {
            var campaign = await _campaignRepository.GetByIdAsync(campaignId);
            if (campaign != null)
            {
                await _campaignRepository.DeleteAsync(campaign);
            }
        }

        // This method triggers the emails for a campaign
        public async Task TriggerCampaignEmailsAsync(int campaignId)
        {
            var campaign = await _campaignRepository.GetByIdAsync(campaignId);
            var template = await _emailTemplateRepository.GetByIdAsync(campaign.EmailTemplateId);

            // Generate and send emails for each client in the campaign (using RabbitMQ for async email sending)
            var clients = ParseClientsFromXml(campaign.XmlData); // Assuming XmlData is parsed to get client emails

            foreach (var client in clients)
            {
                var emailQueueMessage = new EmailQueueMessage
                {
                    ClientId = client.Id,
                    TemplateId = template.Id,
                    TemplateName = "",
                    MarketingDataJson = ""
                };

                // Publish email to RabbitMQ for background email sending
                _rabbitMQPublisher.PublishEmailMessage(emailQueueMessage);
            }
        }

        private List<Client> ParseClientsFromXml(string xmlData)
        {
            // Implement XML parsing logic to convert xmlData to List<Client>
            // This is a placeholder implementation
            return new List<Client>();
        }
    }
}
