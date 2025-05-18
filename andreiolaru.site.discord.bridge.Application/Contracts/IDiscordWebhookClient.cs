using andreiolaru.site.discord.bridge.Domain.Requests;

namespace andreiolaru.site.discord.bridge.Application.Contracts;

public interface IDiscordWebhookClient
{
    Task<bool> SendAsync(DiscordMessageRequest request, CancellationToken ct);
}
