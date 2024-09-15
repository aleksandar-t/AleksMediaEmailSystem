namespace AleksMediaEmailSystem.API.RabbitMQ
{
    public class RabbitMQSettings
    {
        public string HostName { get; set; } // RabbitMQ host name
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public int Port { get; set; }
        public string QueueName { get; set; } // The name of the queue to listen to
        public string ExchangeName { get; set; } // RabbitMQ exchange name
        public string RoutingKey { get; set; } // Routing key for message filtering
    }
}
