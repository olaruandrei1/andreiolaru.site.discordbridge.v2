using System.Threading.Channels;
using andreiolaru.site.discord.bridge.Domain.Requests;
using andreiolaru.site.discord.bridge.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using andreiolaru.site.discord.bridge.Application.Contracts;
using andreiolaru.site.discord.bridge.Domain.Constants;
using andreiolaru.site.discord.bridge.Domain.Entities;

namespace andreiolaru.site.discord.Dispatchers;

public class MessageDispatcherService(Channel<InboxMessage> channel, IServiceProvider provider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var msg in channel.Reader.ReadAllAsync(stoppingToken))
        {
            using var scope = provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var client = scope.ServiceProvider.GetRequiredService<IDiscordWebhookClient>();

            var entity = await db.Messages.FindAsync(new object[] { msg.Id }, cancellationToken: stoppingToken);
            if (entity == null) continue;

            entity.Status = MessageStatus.Sending;
            await db.SaveChangesAsync(stoppingToken);

            var success = await client.SendAsync(new DiscordMessageRequest(entity.From, entity.Subject, entity.Content), stoppingToken);

            entity.Status = success ? MessageStatus.Sent : MessageStatus.Failed;
            entity.SentAt = DateTime.UtcNow;
            await db.SaveChangesAsync(stoppingToken);
        }
    }
}