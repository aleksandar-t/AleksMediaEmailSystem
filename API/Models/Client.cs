namespace AleksMediaEmailSystem.API.Models
{
    public class Client
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } // Client's Name
        public string Email { get; set; } // Client's Email Address
        public bool IsActive { get; set; } // Whether the client is active

        // Configuration details for the email (could include things like frequency, format, etc.)
        public string EmailConfiguration { get; set; } // JSON or any other format for custom configurations

        public DateTime CreatedAt { get; set; } // Date the client was created
        public DateTime? UpdatedAt { get; set; } // Last time the client details were updated
    }
}
