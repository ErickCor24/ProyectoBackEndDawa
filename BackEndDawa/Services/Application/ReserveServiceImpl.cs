using BackEndDawa.Infrastructure;
using BackEndDawa.Models;
using BackEndDawa.Services.Ports;
using Microsoft.EntityFrameworkCore;

namespace BackEndDawa.Services.Application
{
    public class ReserveServiceImpl : IReserve
    {
        private readonly ContextConnection _context;

        public ReserveServiceImpl(ContextConnection context)
        {
            _context = context;
        }

        public async Task<Reserve?> CreateReserveAsync(Reserve reserve)
        {
            // Validar si el vehículo existe y está disponible
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == reserve.VehicleId && v.IsAvailable);
            if (vehicle == null)
                return null;

            // Validar conflicto de fechas con reservas activas
            var conflict = await _context.Reserves.AnyAsync(r =>
                r.VehicleId == reserve.VehicleId &&
                r.status == true &&
                !(reserve.DropoffDate <= r.PickupDate || reserve.PickupDate >= r.DropoffDate)
            );
            if (conflict)
                return null;

            // Calcular precio
            var days = (reserve.DropoffDate - reserve.PickupDate).Days;
            reserve.Price = days * vehicle.PricePerDay;
            reserve.status = true;

            // Guardar reserva
            _context.Reserves.Add(reserve);

            // Marcar vehículo como no disponible
            vehicle.IsAvailable = false;
            _context.Vehicles.Update(vehicle);

            await _context.SaveChangesAsync();
            return reserve;
        }
        public async Task<IEnumerable<object>> GetAllReservesAsync()
        {
            return await _context.Reserves
                .Include(r => r.Client)
                .Include(r => r.Vehicle)
                .Select(r => new
                {
                    r.Id,
                    r.PickupDate,
                    r.DropoffDate,
                    r.Price,
                    r.status,
                    ClientId = r.ClientId,
                    VehicleId = r.VehicleId,
                    Client = new
                    {
                        r.Client.Id,
                        r.Client.FullName,
                        r.Client.Email,
                        r.Client.PhoneNumber
                    },
                    Vehicle = new
                    {
                        r.Vehicle.Id,
                        r.Vehicle.Brand,
                        r.Vehicle.Model,
                        r.Vehicle.PlateNumber,
                        r.Vehicle.PricePerDay
                    }
                })
                .ToListAsync();
        }

        public async Task<Reserve?> GetReserveByIdAsync(int id)
        {
            return await _context.Reserves
                .Include(r => r.Client)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> UpdateReserveAsync(Reserve updatedReserve)
        {
            var existing = await _context.Reserves.FindAsync(updatedReserve.Id);
            if (existing == null)
                return false;

            // Verifica conflicto de fechas con otras reservas del mismo vehículo (excluyendo esta)
            bool conflicto = await _context.Reserves.AnyAsync(r =>
                r.Id != updatedReserve.Id &&
                r.VehicleId == updatedReserve.VehicleId &&
                r.status == true &&
                r.PickupDate < updatedReserve.DropoffDate &&
                updatedReserve.PickupDate < r.DropoffDate
            );

            if (conflicto)
                return false;

            // Recalcular el precio
            var vehicle = await _context.Vehicles.FindAsync(updatedReserve.VehicleId);
            if (vehicle == null || !vehicle.Status)
                return false;

            int days = (int)(updatedReserve.DropoffDate - updatedReserve.PickupDate).TotalDays;
            updatedReserve.Price = days * vehicle.PricePerDay;

            // Actualiza propiedades permitidas
            existing.PickupDate = updatedReserve.PickupDate;
            existing.DropoffDate = updatedReserve.DropoffDate;
            existing.Price = updatedReserve.Price;
            existing.status = updatedReserve.status;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteReserveAsync(int id)
        {
            var reserve = await _context.Reserves.FindAsync(id);
            if (reserve == null)
                return false;

            _context.Reserves.Remove(reserve);
            await _context.SaveChangesAsync();
            return true;
        }

    }

}
