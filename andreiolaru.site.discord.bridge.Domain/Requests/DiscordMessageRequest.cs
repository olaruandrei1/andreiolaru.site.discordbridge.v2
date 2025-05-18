namespace andreiolaru.site.discord.bridge.Domain.Requests;

public record DiscordMessageRequest(string From, string Subject, string Message);
