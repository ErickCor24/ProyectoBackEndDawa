using BackEndDawa.Models;

namespace BackEndDawa.Services.Ports
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(int id);
        Task<IEnumerable<Vehicle>> GetVehiclesByCompanyAsync(int companyId);
        Task<IEnumerable<Vehicle>> GetVehiclesByField(string field, string value);
        Task<Vehicle> CreateVehicleAsync(Vehicle vehicle);
        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);
        Task<bool> LogicalDeleteVehicleAsync(int id);
    }
}
