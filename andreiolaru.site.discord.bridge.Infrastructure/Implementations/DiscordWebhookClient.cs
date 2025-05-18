using System.Net.Http.Json;
using andreiolaru.site.discord.bridge.Application.Contracts;
using andreiolaru.site.discord.bridge.Domain.BindingObjects;
using andreiolaru.site.discord.bridge.Domain.Requests;
using Microsoft.Extensions.Options;

namespace andreiolaru.site.discord.bridge.Infrastructure.Implementations;

public class DiscordWebhookClient : IDiscordWebhookClient
{
    private readonly HttpClient _http;
    private readonly string _webhookUrl;

    public DiscordWebhookClient(HttpClient http, IOptions<AppSettings> settings)
    {
        _http = http;
        _webhookUrl = settings.Value.DiscordWebhookUrl;
    }

    public async Task<bool> SendAsync(DiscordMessageRequest request, CancellationToken ct)
    {
        var payload = new
        {
            content = $"From: {request.From}\nSubject: {request.Subject}\n\n{request.Message}"
        };

        var response = await _http.PostAsJsonAsync(_webhookUrl, payload, ct);
        return response.IsSuccessStatusCode;
    }
}
