using BackEndDawa.Infrastructure;
using BackEndDawa.Models;
using BackEndDawa.Services.Ports;
using Microsoft.EntityFrameworkCore;

namespace BackEndDawa.Services.Application
{
    public class VehicleServiceImpl : IVehicleService
    {
        private readonly ContextConnection _context;

        public VehicleServiceImpl(ContextConnection context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            try
            {
                return await _context.Vehicle.Where(v => v.Status == true).ToListAsync();
            } catch (Exception ex)
            {
                throw new Exception($"An error ocurred: {ex.Message}", ex);
            }
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int vehicleId)
        {
            try
            {
                return await _context.Vehicle.FindAsync(vehicleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByCompanyAsync(int companyId)
        {
            try
            {
                return await _context.Vehicle.Where(v => v.CompanyId == companyId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByField(string field, string value)
        {
            try
            {
                var vehicles = _context.Vehicle.AsQueryable();

                switch (field.ToLower().Trim())
                {
                    case "marca":
                        vehicles = vehicles.Where(v => EF.Functions.Like(v.Brand, $"%{value}%"));
                        break;
                    case "modelo":
                        vehicles = vehicles.Where(v => EF.Functions.Like(v.Model, $"%{value}%"));
                        break;
                    case "color":
                        vehicles = vehicles.Where(v => EF.Functions.Like(v.Color, $"%{value}%"));
                        break;
                    case "transmisión":
                    case "transmision":
                        vehicles = vehicles.Where(v => EF.Functions.Like(v.Transmission, $"%{value}%"));
                        break;
                    default:
                        throw new ArgumentException("Invalid field specified");
                }

                return await vehicles.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred: {ex.Message}", ex);
            }
        }

        public async Task<Vehicle> CreateVehicleAsync(Vehicle vehicle)
        {
            try
            {
                _context.Vehicle.Add(vehicle);
                await _context.SaveChangesAsync();
                return vehicle;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred: {ex.Message}", ex);
            }
        }

        public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
        {
            try
            {
                var updatedVehicle = await _context.Vehicle.FindAsync(vehicle.Id);
                if (updatedVehicle == null)
                    throw new KeyNotFoundException("Vehicle not found");

                _context.Entry(updatedVehicle).CurrentValues.SetValues(vehicle);
                await _context.SaveChangesAsync();
                return updatedVehicle;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred: {ex.Message}", ex);
            }

        }

        public async Task<bool> LogicalDeleteVehicleAsync(int vehicleId)
        {
            try
            {
                var deletedVehicle = await _context.Vehicle.FindAsync(vehicleId);
                if (deletedVehicle == null || !deletedVehicle.Status)
                    return false;

                deletedVehicle.Status = false;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred: {ex.Message}", ex);
            }
        }
    }
}
