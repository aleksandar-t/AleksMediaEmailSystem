using System.Xml.Serialization;

namespace AleksMediaEmailSystem.API.Models
{
    [XmlRoot("Clients")]
    public class ClientsData
    {
        [XmlElement("Client")]
        public List<ClientData> Clients { get; set; }
    }

    public class ClientData
    {
        [XmlAttribute("ID")]
        public int ID { get; set; }

        [XmlElement("Template")]
        public TemplateData Template { get; set; }
    }

    public class TemplateData
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("MarketingData")]
        public string MarketingData { get; set; } // This is JSON data represented as string
    }

}
