using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace andreiolaru.site.discord.bridge.Persistence;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    => services.AddDbContext<AppDbContext>(options => options.UseMySQL(config.GetConnectionString("DefaultConnection")));
}