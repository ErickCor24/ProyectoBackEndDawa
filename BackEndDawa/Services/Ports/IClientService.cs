using BackEndDawa.Models;

namespace BackEndDawa.Services.Ports
{
    public interface IClientService
    {
        // Métodos CRUD
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client?> GetClientByIdAsync(int id);
        Task<Client> CreateClientAsync(Client client);
        Task<Client?> UpdateClientAsync(int id, Client client);
        Task<bool> DeleteClientAsync(int id);
        Task<Client?> GetClientByCiAsync(string ci);


        // Métodos de búsqueda
        Task<IEnumerable<Client>> SearchClientsAsync(string? name, string? email, string? ci, string? phone, bool? status);
    }
}
