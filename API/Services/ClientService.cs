using AleksMediaEmailSystem.API.Models;
using AleksMediaEmailSystem.API.Repositories;

namespace AleksMediaEmailSystem.API.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await _clientRepository.GetByIdAsync(clientId);
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task AddClientAsync(Client client)
        {
            await _clientRepository.AddAsync(client);
        }

        public async Task UpdateClientAsync(Client client)
        {
            await _clientRepository.UpdateAsync(client);
        }

        public async Task DeleteClientAsync(int clientId)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);
            if (client != null)
            {
                await _clientRepository.DeleteAsync(client);
            }
        }
    }
}
