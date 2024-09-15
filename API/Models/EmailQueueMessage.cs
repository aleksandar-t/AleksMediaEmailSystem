namespace AleksMediaEmailSystem.API.Models
{
    public class EmailQueueMessage
    {
        public int ClientId { get; set; }
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string MarketingDataJson { get; set; }
    }
}
