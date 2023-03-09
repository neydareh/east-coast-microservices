using FluentValidation;
using MediatR;
using ValidationException = Ordering.Application.Exception.ValidationException;

namespace Ordering.Application.Behavior;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull {
  private readonly IEnumerable<IValidator<TRequest>> _validators;

  public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) {
    _validators = validators ?? throw new ArgumentNullException(nameof(validators));
  }

  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken) {
    if (!_validators.Any()) return await next();
    var context = new ValidationContext<TRequest>(request);
    var validationResults = await Task.WhenAll(
      _validators.Select(x => x.ValidateAsync(context, cancellationToken))
    );
    var failures = validationResults.SelectMany(x => x.Errors).Where(x => x != null).ToList();

    if (failures.Count != 0) {
      throw new ValidationException(failures);
    }

    return await next();
  }
}