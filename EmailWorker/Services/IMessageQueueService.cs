using AleksMediaEmailSystem.API.Models;

namespace AleksMediaEmailSystem.EmailWorker.Services
{
    public interface IMessageQueueService
    {
        void PublishMessage<T>(T message, string queueName);
        void PublishEmailMessage(EmailQueueMessage emailQueueMessage);
        Task SendMessageAsync(EmailMessage message);
        Task<EmailQueueMessage> ReceiveMessageAsync();
        EmailQueueMessage ReceiveMessage();
        Task<List<EmailQueueMessage>> GetEmailMessagesAsync(int batchSize = 100);
    }
}
