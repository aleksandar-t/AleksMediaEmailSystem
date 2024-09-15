namespace AleksMediaEmailSystem.API.Models
{
    public class EmailStatus
    {
        public int EmailStatusId { get; set; } // Primary Key
        public int ClientId { get; set; } // Reference to the client
        public int EmailTemplateId { get; set; } // Reference to the email template
        public DateTime SentAt { get; set; } // Date the email was sent
        public bool Success { get; set; } // Whether the email was successfully sent
        public string FailureReason { get; set; } // Reason for failure (if applicable)
    }
}
