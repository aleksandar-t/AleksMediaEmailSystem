using AleksMediaEmailSystem.API.Models;

namespace AleksMediaEmailSystem.API.Services
{
    public interface IXmlParserService
    {
        ClientsData ParseClientsFromXml(string xmlFilePath);
    }
}