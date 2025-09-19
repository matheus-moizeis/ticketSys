
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketSys.Application.Interfaces;
using TicketSys.Application.Mappings;
using TicketSys.Application.Services;
using TicketSys.Domain.Interfaces;
using TicketSys.Infra.Data.Context;
using TicketSys.Infra.Data.Repositories;

namespace TicketSys.Infra.IoC;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

        services.AddScoped<IUnitRepository, UnitRepository>();
        services.AddScoped<IUnitService, UnitService>();
        services.AddAutoMapper(cfg => cfg.AddProfile<DomainToDTOMappingProfile>());


        return services;

    }
}
