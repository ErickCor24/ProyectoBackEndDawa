using BackEndDawa.Models;

namespace BackEndDawa.Services.Ports
{
    public interface ICompany
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();

    }
}
