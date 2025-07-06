using BackEndDawa.Models;

namespace BackEndDawa.Services.Ports
{
    public interface IUserClient
    {
        Task<UserClient> LoginUser(User user);
        Task<UserClient> RegisterUser(Client client, string passwoord);
    }
}
