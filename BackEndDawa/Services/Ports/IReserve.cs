using BackEndDawa.Models;
using System.Threading.Tasks;


namespace BackEndDawa.Services.Ports
{
    public interface IReserve
    {
        Task<Reserve?> CreateReserveAsync(Reserve reserve);
        Task<IEnumerable<object>> GetAllReservesAsync();
        Task<Reserve?> GetReserveByIdAsync(int id);
        Task<bool> UpdateReserveAsync(Reserve reserve);
        Task<bool> DeleteReserveAsync(int id);

    }

}
