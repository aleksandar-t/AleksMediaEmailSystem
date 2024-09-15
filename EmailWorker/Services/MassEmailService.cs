using AleksMediaEmailSystem.API.Models;
using AleksMediaEmailSystem.API.Services;

namespace AleksMediaEmailSystem.EmailWorker.Services
{
    public class MassEmailService : IMassEmailService
    {
        private readonly IXmlParserService _xmlParserService;
        private readonly IMessageQueueService _messagePublisher;

        public MassEmailService(XmlParserService xmlParserService, IMessageQueueService messagePublisher)
        {
            _xmlParserService = xmlParserService;
            _messagePublisher = messagePublisher;
        }

        public void ProcessXmlForCampaign(string xmlFilePath)
        {
            // Parse the XML file
            ClientsData clientsData = _xmlParserService.ParseClientsFromXml(xmlFilePath);

            // Enqueue each email for sending
            foreach (var client in clientsData.Clients)
            {
                var emailQueueMessage = new EmailQueueMessage
                {
                    ClientId = client.ID,
                    TemplateId = client.Template.Id,
                    TemplateName = client.Template.Name,
                    MarketingDataJson = client.Template.MarketingData
                };

                // Publish message to RabbitMQ
                _messagePublisher.PublishEmailMessage(emailQueueMessage);
            }
        }
    }
}
