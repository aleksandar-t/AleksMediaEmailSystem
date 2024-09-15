using AleksMediaEmailSystem.API.Models;

namespace AleksMediaEmailSystem.API.Services
{
    public interface IClientService
    {
        Task<Client> GetClientByIdAsync(int clientId);
        Task<List<Client>> GetAllClientsAsync();
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int clientId);
    }
}
