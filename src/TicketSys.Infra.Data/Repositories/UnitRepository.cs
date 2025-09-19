using Microsoft.EntityFrameworkCore;
using TicketSys.Domain.Entities;
using TicketSys.Domain.Interfaces;
using TicketSys.Infra.Data.Context;

namespace TicketSys.Infra.Data.Repositories;

public class UnitRepository(ApplicationDbContext context) : IUnitRepository
{
    readonly ApplicationDbContext _context = context;

    public async Task<Unit> CreateAsync(Unit unit)
    {
        await _context.Units.AddAsync(unit);
        await _context.SaveChangesAsync();
        return unit;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var unit = await _context.Units.FirstOrDefaultAsync(x => x.Id == id);
        _context.Units.Remove(unit!);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Unit>> GetAllAsync()
    {
        return await _context.Units.ToListAsync();
    }

    public async Task<Unit?> GetByIdAsync(int? id)
    {
        return await _context.Units.FindAsync(id);
    }

    public async Task<Unit> UpdateAsync(Unit unit)
    {
        _context.Update(unit);
        await _context.SaveChangesAsync();
        return unit;
    }
}
