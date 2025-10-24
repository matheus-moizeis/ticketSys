
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketSys.Application.Interfaces;
using TicketSys.Application.Mappings;
using TicketSys.Application.Services;
using TicketSys.Domain.Account;
using TicketSys.Domain.Interfaces;
using TicketSys.Infra.Data.Context;
using TicketSys.Infra.Data.Identity;
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

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IUnitRepository, UnitRepository>();
        services.AddScoped<IUnitService, UnitService>();
        services.AddScoped<IAuthenticate, AuthenticateService>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        services.AddAutoMapper(cfg => cfg.AddProfile<DomainToDTOMappingProfile>());

        return services;

    }
}
