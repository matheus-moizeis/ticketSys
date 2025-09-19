using AutoMapper;
using TicketSys.Application.DTOs;
using TicketSys.Application.Interfaces;
using TicketSys.Domain.Entities;
using TicketSys.Domain.Interfaces;

namespace TicketSys.Application.Services;

public class UnitService(IUnitRepository unitRepository, IMapper mapper) : IUnitService
{
    private readonly IUnitRepository _unitRepository = unitRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<UnitDTO>> GetAllUnits()
    {
        var unitsEntity = await _unitRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UnitDTO>>(unitsEntity);
    }

    public async Task<UnitDTO> GetById(int id)
    {
        var unitEntity = await _unitRepository.GetByIdAsync(id);
        return _mapper.Map<UnitDTO>(unitEntity);
    }

    public async Task Add(UnitDTO unitDto)
    {
        var unitEntity = _mapper.Map<Unit>(unitDto);
        await _unitRepository.CreateAsync(unitEntity);
    }

    public async Task Update(UnitDTO unitDto)
    {
        var unitEntity = _mapper.Map<Unit>(unitDto);
        await _unitRepository.UpdateAsync(unitEntity);
    }

    public async Task Remove(int id)
    {
        await _unitRepository.DeleteAsync(id);
    }
}
