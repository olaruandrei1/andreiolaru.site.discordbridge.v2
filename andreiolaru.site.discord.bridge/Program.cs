using andreiolaru.site.discord.bridge.Application;
using andreiolaru.site.discord.bridge.Application.Features.DiscordMessage;
using andreiolaru.site.discord.bridge.Domain;
using andreiolaru.site.discord.bridge.Domain.Requests;
using andreiolaru.site.discord.bridge.Infrastructure;
using andreiolaru.site.discord.bridge.Middlewares;
using andreiolaru.site.discord.bridge.Persistence;
using andreiolaru.site.discord.Dispatchers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddDispatchers();
builder.Services.AddDomain(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ApiKeyMiddleware>();

app.MapPost("/api/send_message", async (
    [FromBody] DiscordMessageRequest request,
    IMediator mediator,
    IMapper mapper) =>
{
    var command = mapper.Map<DiscordMessageCommand>(request);
    
    var response = await mediator.Send(command);
    
    return Results.Ok(response);
});

app.Run();