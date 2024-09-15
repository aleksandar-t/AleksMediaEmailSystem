using System.ComponentModel.DataAnnotations;

namespace AleksMediaEmailSystem.API.Models
{
    public class Campaign
    {
        public int Id { get; set; } // Primary Key
        public string CampaignName { get; set; } // Name of the Campaign    
        public string Description { get; set; } // Description of the Campaign
        public string XmlData { get; set; } // Uploaded XML data for the campaign, could store file reference or the data itself
        public DateTime StartDate { get; set; } // Date the campaign was created
        public DateTime? EndDate { get; set; } // When the campaign completed
        public bool IsActive { get; set; } // Whether the campaign is currently active
        public int EmailTemplateId { get; set; } // Foreign Key
        public EmailTemplate EmailTemplate { get; set; } // Navigation Property
    }
}
