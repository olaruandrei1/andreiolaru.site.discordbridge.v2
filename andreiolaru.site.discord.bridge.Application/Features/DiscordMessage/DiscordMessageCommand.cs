using System.Threading.Channels;
using andreiolaru.site.discord.bridge.Domain.Constants;
using andreiolaru.site.discord.bridge.Domain.Entities;
using andreiolaru.site.discord.bridge.Domain.Responses;
using andreiolaru.site.discord.bridge.Persistence;
using MediatR;

namespace andreiolaru.site.discord.bridge.Application.Features.DiscordMessage;

public class DiscordMessageCommand : IRequest<DiscordMessageResponse>
{
    public string From { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Message { get; set; } = default!;
}

public class DiscordMessageCommandHandler(AppDbContext db, Channel<InboxMessage> channel)
    : IRequestHandler<DiscordMessageCommand, DiscordMessageResponse>
{
    public async Task<DiscordMessageResponse> Handle(DiscordMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new InboxMessage
        {
            Id = Guid.NewGuid(),
            From = request.From,
            Subject = request.Subject,
            Content = request.Message,
            Status = MessageStatus.Queued,
            CreatedAt = DateTime.UtcNow
        };

        db.Messages.Add(message);
        await db.SaveChangesAsync(cancellationToken);

        Task.Run(async () => await channel.Writer.WriteAsync(message, cancellationToken));

        return new DiscordMessageResponse(message.Id, message.Status.ToString());
    }
}