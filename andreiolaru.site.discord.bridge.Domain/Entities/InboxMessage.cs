using andreiolaru.site.discord.bridge.Domain.Constants;

namespace andreiolaru.site.discord.bridge.Domain.Entities;

public class InboxMessage
{
    public Guid Id { get; set; }
    public string From { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Content { get; set; } = default!;
    public MessageStatus Status { get; set; } = MessageStatus.Queued;
    public DateTime CreatedAt { get; set; }
    public DateTime? SentAt { get; set; }
}
