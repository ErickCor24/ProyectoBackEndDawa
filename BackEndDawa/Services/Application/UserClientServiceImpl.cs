using BackEndDawa.Infrastructure;
using BackEndDawa.Models;
using BackEndDawa.Services.Ports;
using Microsoft.EntityFrameworkCore;

namespace BackEndDawa.Services.Application
{
    public class UserClientServiceImpl : IUserClient
    {
        private readonly ContextConnection _context;

        public UserClientServiceImpl(ContextConnection context)
        {
            _context = context;
        }

        public async Task<UserClient> LoginUser(User user)
        {
            var userFound = await _context.UserClients
                .FirstOrDefaultAsync(u => u.UserName == user.Email && u.Password == user.Password);

            if (userFound != null)
            {
                return userFound;
            }
            else throw new Exception("User not found");
        }

        public async Task<UserClient> RegisterUser(Client client, string passwoord)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            var lastId = client.Id;

            var user = new UserClient
            {
                UserName = client.Email,
                Password = passwoord,
                Client = client,
                ClientId = lastId
            };

            await _context.UserClients.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
