using DotNetEnv;
using TicketSys.Domain.Account;
using TicketSys.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

Env.Load();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

SeedUserRoles(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();


static void SeedUserRoles(IApplicationBuilder app)
{
    using var serviceScope = app.ApplicationServices.CreateScope();
    var seed = serviceScope.ServiceProvider
                           .GetService<ISeedUserRoleInitial>();
    seed!.SeedRoles();
    seed!.SeedUsers();
}
