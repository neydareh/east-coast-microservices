using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behavior;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull{
  private readonly ILogger<TRequest> _logger;

  public UnhandledExceptionBehavior(ILogger<TRequest> logger) {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken) {
    try {
      return await next();
    }
    catch (System.Exception e) {
      var requestName = typeof(TRequest).Name;
      _logger.LogError(e, $"Application Request: Unhandled Exception for Request {requestName}, {request}");
      throw;
    }
  }
}