using AleksMediaEmailSystem.API.Models;
using AleksMediaEmailSystem.API.Services;
using AleksMediaEmailSystem.EmailWorker.Helpers;
using AleksMediaEmailSystem.EmailWorker.Services;

namespace AleksMediaEmailSystem.EmailWorker.Workers
{
    class EmailSenderWorker : BackgroundService
    {
        private readonly IMessageQueueService _messageQueueService;
        private readonly IEmailService _emailService;
        private readonly IClientService _clientService;
        private readonly ILogger<EmailSenderWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        //public EmailSenderWorker(IMessageQueueService messageQueueService, IEmailService emailService, ILogger<EmailSenderWorker> logger, IClientService clientService, IServiceProvider serviceProvider)
        //{
        //    _messageQueueService = messageQueueService;
        //    _emailService = emailService;
        //    _clientService = clientService;
        //    _logger = logger;
        //    _serviceProvider = serviceProvider;
        //}

        public EmailSenderWorker(ILogger<EmailSenderWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("EmailSenderWorker is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {

                try
                {
                    // Process emails concurrently
                    await ProcessEmailsConcurrently(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing emails");
                }

                // Delay between batches (can be configured)
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);



                //single email processing
                //if (messageQueueMsg != null)
                //{
                //    //messageQueueMsg = _messageQueueService.ReceiveMessageAsync(stoppingToken).Result;

                //    using (IServiceScope scope = _serviceProvider.CreateScope())
                //    {
                //        IClientService scopedProcessingService =
                //            scope.ServiceProvider.GetRequiredService<IClientService>();

                //        client = await scopedProcessingService.GetClientByIdAsync(messageQueueMsg.ClientId);
                //    }

                //    //client = _clientService.GetClientByIdAsync(messageQueueMsg.ClientId).Result;

                //    var template = new TemplateRenderer();
                //    var emailBody = template.RenderTemplate(messageQueueMsg.TemplateName, messageQueueMsg.MarketingDataJson);
                //    //var emailBody = await template.RenderTemplateAsync(messageQueueMsg.TemplateName, messageQueueMsg.MarketingDataJson);

                //    var message = new Models.EmailMessage()
                //    {
                //        EmailAddress = client.Email,
                //        Subject = "Test email",
                //        Body = emailBody
                //    };

                //    if (message != null)
                //    {
                //        try
                //        {
                //            _logger.LogInformation($"Processing email for: {message.EmailAddress}");
                //            using (IServiceScope scope = _serviceProvider.CreateScope())
                //            {
                //                IEmailService scopedProcessingService =
                //                    scope.ServiceProvider.GetRequiredService<IEmailService>();

                //                await scopedProcessingService.SendEmailDemoAsync(message);
                //            }
                //            //_emailService.SendEmailDemoAsync(message);
                //        }
                //        catch (Exception ex)
                //        {
                //            _logger.LogError(ex, "Error sending email");
                //        }
                //    }
                //}

            }

            _logger.LogInformation("EmailSenderWorker is stopping.");

        }

        // Process emails concurrently using multithreading
        private async Task ProcessEmailsConcurrently(CancellationToken stoppingToken)
        {

            EmailQueueMessage messageQueueMsg = new EmailQueueMessage();
            List<EmailQueueMessage> messageQueueMsgs = new List<EmailQueueMessage>();
            Client client = new Client();

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IMessageQueueService scopedProcessingService =
                    scope.ServiceProvider.GetRequiredService<IMessageQueueService>();

                messageQueueMsgs = await scopedProcessingService.GetEmailMessagesAsync(100);
            }
              
            var emailMessages = new List<Models.EmailMessage>();

            foreach (var msg in messageQueueMsgs)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IClientService scopedProcessingService =
                        scope.ServiceProvider.GetRequiredService<IClientService>();

                    client = await scopedProcessingService.GetClientByIdAsync(msg.ClientId);
                }

                var template = new TemplateRenderer();
                var emailBody = template.RenderTemplate(msg.TemplateName, msg.MarketingDataJson);

                var message = new Models.EmailMessage()
                {
                    EmailAddress = client.Email,
                    Subject = "Test email",
                    Body = emailBody
                };

                emailMessages.Add(message);
            }

            var tasks = emailMessages.Select(emailMessage =>
            {
                // Run email sending operation in parallel tasks
                return Task.Run(async () =>
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                        await emailService.SendEmailDemoAsync(emailMessage);
                    }
                }, stoppingToken);
            });


            // Wait for all email sending tasks to complete
            await Task.WhenAll(tasks);
        }

    }

}
