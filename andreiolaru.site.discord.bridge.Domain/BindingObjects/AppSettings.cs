namespace andreiolaru.site.discord.bridge.Domain.BindingObjects;

public class AppSettings
{
    public string DiscordWebhookUrl { get; init; } = default!;
    public string ApiKey { get; init; } = default!;
}
