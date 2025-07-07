using BackEndDawa.Models;

namespace BackEndDawa.Services.Ports
{
    public interface IUserCompany
    {

        public Task<UserCompany> LoginUser(User user);
        public Task<UserCompany> RegisterUser(Company company, string passwoord);

    }
}
