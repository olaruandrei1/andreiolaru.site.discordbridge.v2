using andreiolaru.site.discord.bridge.Application.Features.DiscordMessage;
using andreiolaru.site.discord.bridge.Domain.Requests;
using AutoMapper;

namespace andreiolaru.site.discord.bridge.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DiscordMessageRequest, DiscordMessageCommand>();
    }
}