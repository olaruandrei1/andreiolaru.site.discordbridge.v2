using andreiolaru.site.discord.bridge.Application.Contracts;
using andreiolaru.site.discord.bridge.Infrastructure.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace andreiolaru.site.discord.bridge.Infrastructure;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpClient<IDiscordWebhookClient, DiscordWebhookClient>();
        return services;
    }
}
