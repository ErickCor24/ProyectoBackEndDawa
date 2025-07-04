using BackEndDawa.Infrastructure;
using BackEndDawa.Models;
using BackEndDawa.Services.Ports;
using Microsoft.EntityFrameworkCore;

namespace BackEndDawa.Services.Application
{
    public class CompanyServiceImpl : ICompany
    {

        private readonly ContextConnection _context;

        public CompanyServiceImpl(ContextConnection context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            try
            {
            return await _context.Companies.ToListAsync();

            } catch (Exception ex)
            {
                throw new Exception("An error occured while retrieving companies", ex);
            }

        }
    }
}
