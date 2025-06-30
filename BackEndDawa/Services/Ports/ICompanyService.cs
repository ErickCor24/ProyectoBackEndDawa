using BackEndDawa.Models;

namespace BackEndDawa.Services.Ports
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();

    }
}
