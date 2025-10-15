using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TicketSys.Infra.Data.Context;

namespace TicketSys.Infra.Data.Factories;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Caminho base do projeto onde está o appsettings.json
        // var basePath = Directory.GetCurrentDirectory();

        // Carrega a configuração
        // var configuration = new ConfigurationBuilder()
        //     .SetBasePath(basePath)
        //     .AddJsonFile("appsettings.json", optional: false)
        //     .Build();

        // var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql("string de conexão aqui",
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
