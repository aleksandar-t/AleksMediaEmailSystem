using AleksMediaEmailSystem.API.Models;

namespace AleksMediaEmailSystem.API.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(Database.ApplicationDbContext context) : base(context)
        {
        }

        // Add any client-specific methods here
    }
}
