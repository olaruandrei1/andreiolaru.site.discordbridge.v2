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
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddDispatchers();
builder.Services.AddDomain(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var retryCount = 0;
    var connected = false;
    while (!connected && retryCount < 10)
    {
        try
        {
            db.Database.Migrate();
            connected = true;
        }
        catch
        {
            retryCount++;
            Console.WriteLine($"Database not ready yet... retrying ({retryCount}/10)");
            Thread.Sleep(3000);
        }
    }
}


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