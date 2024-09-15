using AleksMediaEmailSystem.API.Services;

namespace AleksMediaEmailSystem.EmailWorker.Services
{
    public interface IMassEmailService
    {
        void ProcessXmlForCampaign(string xmlFilePath);
    }
}