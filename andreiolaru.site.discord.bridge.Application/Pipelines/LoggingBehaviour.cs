using MediatR;

namespace andreiolaru.site.discord.bridge.Application.Pipelines;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[LOG] Handling {typeof(TRequest).Name}");
        
        var response = await next();
        
        Console.WriteLine($"[LOG] Handled {typeof(TRequest).Name}");
        
        return response;
    }
}