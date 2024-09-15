namespace AleksMediaEmailSystem.API.Models
{
    public class ClientEmailConfiguration
    {
        public int ClientId { get; set; } // Reference to the client
        public string ConfigKey { get; set; } // Configuration key (e.g., "emailFrequency")
        public string ConfigValue { get; set; } // Configuration value (e.g., "daily", "weekly")
    }
}
