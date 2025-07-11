using BackEndDawa.Infrastructure;
using BackEndDawa.Models;
using BackEndDawa.Services.Ports;
using Microsoft.EntityFrameworkCore;


namespace BackEndDawa.Services.Application
{
    public class ClientServiceImpl : IClientService
    {
        private readonly ContextConnection _context;

        public ClientServiceImpl(ContextConnection context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            try
            {
                return await _context.Clients
                    .Where(c => c.Status == true)
                    .OrderBy(c => c.FullName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al recuperar clientes", ex);
            }
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            try
            {
                return await _context.Clients
                    .FirstOrDefaultAsync(c => c.Id == id && c.Status == true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Se produjo un error al recuperar el cliente con id {id}", ex);
            }
        }

        public async Task<Client?> GetClientByCiAsync(string ci)
        {
            try
            {
                return await _context.Clients
                    .FirstOrDefaultAsync(c => c.Ci == ci && c.Status == true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Se produjo un error al recuperar el cliente con Ci {ci}", ex);
            }
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingClient = await _context.Clients
                    .FirstOrDefaultAsync(c => c.Email == client.Email);
                if (existingClient != null)
                {
                    throw new Exception("Ya existe un cliente con este correo electronico");
                }

                client.Status = true;
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();

                var userClient = new UserClient
                {
                    UserName = client.Email,
                    Password = client.Password,
                    Status = true,
                    ClientId = client.Id
                };

                _context.UserClients.Add(userClient);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return client;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                var errorMessage = $"Error: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $" | Inner: {ex.InnerException.Message}";
                }
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<Client?> UpdateClientAsync(int id, Client client)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingClient = await _context.Clients
                    .FirstOrDefaultAsync(c => c.Id == id && c.Status == true);
                if (existingClient == null) return null;

                if (existingClient.Email != client.Email)
                {
                    var emailExists = await _context.Clients
                        .AnyAsync(c => c.Email == client.Email && c.Id != id);
                    if (emailExists)
                    {
                        throw new Exception("Ya existe un cliente con este correo electronico");
                    }
                }

                existingClient.FullName = client.FullName;
                existingClient.Email = client.Email;
                existingClient.PhoneNumber = client.PhoneNumber;
                existingClient.Ci = client.Ci;
                existingClient.Address = client.Address;
                existingClient.BirthDate = client.BirthDate;
                existingClient.Nationality = client.Nationality;

                var userClient = await _context.UserClients
                    .FirstOrDefaultAsync(uc => uc.ClientId == id);

                if (userClient != null && userClient.UserName != client.Email)
                {
                    userClient.UserName = client.Email;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return existingClient;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Se produjo un error al actualizar el cliente  {id}", ex);
            }
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var client = await _context.Clients
                    .FirstOrDefaultAsync(c => c.Id == id && c.Status == true);
                if (client == null) return false;

                client.Status = false;

                var userClient = await _context.UserClients
                    .FirstOrDefaultAsync(uc => uc.ClientId == id);
                if (userClient != null)
                {
                    userClient.Status = false;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Se produjo un error al eliminar el cliente  {id}", ex);
            }
        }

        public async Task<IEnumerable<Client>> SearchClientsAsync(
            string? name, string? email, string? ci, string? phone, bool? status)
        {
            try
            {
                var query = _context.Set<Client>().AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(c => c.FullName.Contains(name));
                }

                if (!string.IsNullOrEmpty(email))
                {
                    query = query.Where(c => c.Email.Contains(email));
                }

                if (!string.IsNullOrEmpty(ci))
                {
                    query = query.Where(c => c.Ci.Contains(ci));
                }

                if (!string.IsNullOrEmpty(phone))
                {
                    query = query.Where(c => c.PhoneNumber.Contains(phone));
                }

                if (status.HasValue)
                {
                    query = query.Where(c => c.Status == status.Value);
                }
                else
                {
                    query = query.Where(c => c.Status == true);
                }

                return await query.OrderBy(c => c.FullName).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al buscar clientes.", ex);
            }
        }
    }
}
