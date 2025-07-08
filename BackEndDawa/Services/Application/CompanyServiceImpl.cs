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
            return await _context.Companies.Where(c => c.Status == true).ToListAsync();

            } catch (Exception ex)
            {
                throw new Exception("An error occured while retrieving companies", ex);
            }

        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            var result = await _context.Companies.FindAsync(id);

            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception($"Company with ID {id} not found.");
            }
        }

        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            try
            {
                var existingCompany = await GetCompanyByIdAsync(company.Id);
                _context.Entry(existingCompany).CurrentValues.SetValues(company);

                var useraccount = new UserCompany { Id = company.Id, Email = company.Email };
                _context.UserCompanies.Attach(useraccount);
                _context.Entry(useraccount).Property(u => u.Email).IsModified = true;

                await _context.SaveChangesAsync();
                return existingCompany;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the company: {ex.Message}", ex);
            }
        }

        public Task<Company> DeletCompanyAsync(int id)
        {
            var company = _context.Companies.Find(id);

            if(company == null) throw new Exception($"Company with ID {id} not found.");

            company.Status = false;
            _context.Companies.Update(company);
            _context.SaveChangesAsync();
            return Task.FromResult(company);
        }

        public async Task<IEnumerable<Company>> GetCompanyByNameAsync(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return await _context.Companies.Where(c => c.Status == true).ToListAsync();
            }
            else
            {
                return await _context.Companies.Where(c => c.Name.ToLower().Contains(name.ToLower()) && c.Status == true).ToListAsync();
            }
        }
    }
}
