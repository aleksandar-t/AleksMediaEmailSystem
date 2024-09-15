namespace AleksMediaEmailSystem.API.Models
{
    public class CampaignEmailRecord
    {
        public int CampaignId { get; set; } // Reference to the campaign
        public int ClientId { get; set; } // Reference to the client receiving the email
        public int EmailTemplateId { get; set; } // Reference to the email template used

        public DateTime SentAt { get; set; } // Date and time when the email was sent
        public bool IsSent { get; set; } // Whether the email has been sent
    }
}
