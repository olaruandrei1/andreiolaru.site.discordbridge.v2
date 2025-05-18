using andreiolaru.site.discord.bridge.Domain.BindingObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace andreiolaru.site.discord.bridge.Domain;

public static class DomainRegistration
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    => services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
}
