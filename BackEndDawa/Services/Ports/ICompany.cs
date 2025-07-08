using BackEndDawa.Models;

namespace BackEndDawa.Services.Ports
{
    public interface ICompany
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();

        Task<Company> GetCompanyByIdAsync(int id);

        Task<IEnumerable<Company>> GetCompanyByNameAsync(string name);

        Task<Company> UpdateCompanyAsync(Company company);

        Task<Company> DeletCompanyAsync(int id);

    }
}
