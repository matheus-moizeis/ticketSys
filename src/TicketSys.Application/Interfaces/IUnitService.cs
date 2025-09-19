using TicketSys.Application.DTOs;

namespace TicketSys.Application.Interfaces;

public interface IUnitService
{
    public Task<IEnumerable<UnitDTO>> GetAllUnits();
    public Task<UnitDTO> GetById(int id);
    public Task Add(UnitDTO unitDto);
    public Task Update(UnitDTO unitDto);
    public Task Remove(int id);
}
