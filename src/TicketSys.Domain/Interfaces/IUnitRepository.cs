using TicketSys.Domain.Entities;

namespace TicketSys.Domain.Interfaces;

public interface IUnitRepository
{
    Task<IEnumerable<Unit>> GetAllAsync();
    Task<Unit?> GetByIdAsync(int? id);
    Task<Unit> CreateAsync(Unit unit);
    Task<Unit> UpdateAsync(Unit unit);
    Task<bool> DeleteAsync(int id);
}
