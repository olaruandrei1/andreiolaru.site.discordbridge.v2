using FluentValidation;

namespace andreiolaru.site.discord.bridge.Application.Features.DiscordMessage;

public class DiscordMessageCommandValidator : AbstractValidator<DiscordMessageCommand>
{
    public DiscordMessageCommandValidator()
    {
        RuleFor(x => x.From).NotEmpty().EmailAddress();
        RuleFor(x => x.Subject).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Message).NotEmpty().MaximumLength(2000);
    }
}