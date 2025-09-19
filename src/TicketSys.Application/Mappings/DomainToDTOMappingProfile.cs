using AutoMapper;
using TicketSys.Application.DTOs;
using TicketSys.Domain.Entities;

namespace TicketSys.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Unit, UnitDTO>().ReverseMap();
    }
}
