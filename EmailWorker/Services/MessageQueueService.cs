using System.Text;
using RabbitMQ.Client;
using AleksMediaEmailSystem.API.Models;
using AleksMediaEmailSystem.API.RabbitMQ;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AleksMediaEmailSystem.EmailWorker.Services
{
    public class MessageQueueService : IMessageQueueService
    {
        private readonly RabbitMQSettings _rabbitMQConfig;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageQueueService(IOptions<RabbitMQSettings> options)
        {
            _rabbitMQConfig = options.Value;
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQConfig.HostName,
                UserName = _rabbitMQConfig.UserName,
                Password = _rabbitMQConfig.Password,
                VirtualHost = _rabbitMQConfig.VirtualHost,
                Port = _rabbitMQConfig.Port
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void PublishMessage<T>(T message, string queueName)
        {
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: messageBody);
        }

        public void PublishEmailMessage(EmailQueueMessage emailQueueMessage)
        {
            var factory = new ConnectionFactory() { HostName = _rabbitMQConfig.HostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMQConfig.QueueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var messageBody = JsonSerializer.Serialize(emailQueueMessage);
                var body = Encoding.UTF8.GetBytes(messageBody);

                channel.BasicPublish(exchange: _rabbitMQConfig.ExchangeName,
                                     routingKey: _rabbitMQConfig.RoutingKey,
                                     basicProperties: null,
                                     body: body);
            }
        }

        public Task SendMessageAsync(EmailMessage message)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            _channel.BasicPublish(exchange: _rabbitMQConfig.ExchangeName, routingKey: _rabbitMQConfig.RoutingKey, basicProperties: null, body: body);
            return Task.CompletedTask;
        }

        public Task<EmailQueueMessage> ReceiveMessageAsync()
        {
            var consumer = new EventingBasicConsumer(_channel);
            var message = new TaskCompletionSource<EmailQueueMessage>();

            consumer.Received += (model, ea) =>
            {
                if (message.Task.IsCompleted)
                    message = new TaskCompletionSource<EmailQueueMessage>();
                
                    var body = ea.Body.ToArray();
                    var jsonMessage = Encoding.UTF8.GetString(body);
                    var emailMessage = JsonSerializer.Deserialize<EmailQueueMessage>(jsonMessage);
                    message.SetResult(emailMessage);
                
            };

            _channel.BasicConsume(queue: _rabbitMQConfig.QueueName, autoAck: true, consumer: consumer);

            return message.Task;
        }

        public async Task<List<EmailQueueMessage>> GetEmailMessagesAsync(int batchSize = 100)
        {
            var emailMessages = new List<EmailQueueMessage>();

            for (int i = 0; i < batchSize; i++)
            {
                var result = _channel.BasicGet(_rabbitMQConfig.QueueName, true);

                if (result != null)
                {
                    var messageBody = Encoding.UTF8.GetString(result.Body.ToArray());
                    var emailMessage = JsonConvert.DeserializeObject<EmailQueueMessage>(messageBody);
                    if (emailMessage != null)
                    {
                        emailMessages.Add(emailMessage);
                    }
                }
                else
                {
                    // No more messages in the queue, break out of the loop
                    break;
                }
            }

            return await Task.FromResult(emailMessages);
        }

        public EmailQueueMessage ReceiveMessage()
        {
            var consumer = new EventingBasicConsumer(_channel);
            EmailQueueMessage message = null;

            consumer.Received += (model, ea) =>
            {
                //if (message.Task.IsCompleted)
                //    message = new TaskCompletionSource<EmailQueueMessage>();

                var body = ea.Body.ToArray();
                var jsonMessage = Encoding.UTF8.GetString(body);
                var emailMessage = JsonSerializer.Deserialize<EmailQueueMessage>(jsonMessage);
                message = emailMessage;

            };

            _channel.BasicConsume(queue: _rabbitMQConfig.QueueName, autoAck: true, consumer: consumer);

            return message;
        }

        //public void Dispose()
        //{
        //    _channel?.Dispose();
        //    _connection?.Dispose();
        //}
    }
}
