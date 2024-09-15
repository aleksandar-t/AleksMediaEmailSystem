namespace AleksMediaEmailSystem.API.Models
{
    public class EmailTemplate
    {
        public int Id { get; set; } // Primary Key
        public string TemplateName { get; set; } // Template Name (for easy identification)
        public string Subject { get; set; } // Email Subject
        public string Body { get; set; } // HTML content for the body of the email
        public DateTime CreatedAt { get; set; } // Date the template was created
        public DateTime? UpdatedAt { get; set; } // Date the template was last updated
        public bool IsActive { get; set; } // Whether the template is active
    }
}
