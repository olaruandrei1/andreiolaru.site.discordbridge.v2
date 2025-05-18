using System.Threading.Channels;
using andreiolaru.site.discord.bridge.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace andreiolaru.site.discord.Dispatchers;

public static class DispatcherRegistration
{
    public static IServiceCollection AddDispatchers(this IServiceCollection services)
    => services.AddSingleton(Channel.CreateUnbounded<InboxMessage>())
               .AddHostedService<MessageDispatcherService>();
}