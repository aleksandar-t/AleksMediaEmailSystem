namespace AleksMediaEmailSystem.API.Models
{
    public class EmailMessage
    {
        public string To { get; set; } // Recipient's email address
        public string Subject { get; set; } // Email subject
        public string Body { get; set; } // Rendered email body (HTML or plain text)
        public bool IsHtml { get; set; } // Whether the email body is HTML or plain text

        public DateTime SentAt { get; set; } // Timestamp of when the email was sent
        public bool Success { get; set; } // Whether the email was successfully sent
        public string FailureReason { get; set; } // Reason for failure (if applicable)
    }
}
