using BackEndDawa.Infrastructure;
using BackEndDawa.Models;
using BackEndDawa.Services.Ports;
using Microsoft.EntityFrameworkCore;

namespace BackEndDawa.Services.Application
{
    public class UserCompanySericeImpl : IUserCompany
    {

        private readonly ContextConnection _context;

        public UserCompanySericeImpl(ContextConnection context)
        {
            _context = context;
        }

        public async Task<UserCompany> LoginUser(User user)
        {
            var userFound = await _context.UserCompanies.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefaultAsync();
            if (userFound != null)
            {
                return userFound;
            }
            else throw new Exception("User not found");
        }

        public async Task<UserCompany> RegisterUser(Company company, string passwoord)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

            var lastId = company.Id;

            var user = new UserCompany
            {
                Email = company.Email,
                Password = passwoord,
                Company = company,
                CompanyId = lastId
            };

            await _context.UserCompanies.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
