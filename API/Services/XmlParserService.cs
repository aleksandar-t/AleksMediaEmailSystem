using AleksMediaEmailSystem.API.Models;
using System.Xml.Serialization;

namespace AleksMediaEmailSystem.API.Services
{
    public class XmlParserService : IXmlParserService
    {
        public ClientsData ParseClientsFromXml(string xmlFilePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ClientsData));

            using (StreamReader reader = new StreamReader(xmlFilePath))
            {
                return (ClientsData)serializer.Deserialize(reader);
            }
        }
    }
}
