using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(next);

            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(validators
                    .Select(x => x.ValidateAsync(context, cancellationToken)));

                var errors = validationResults
                    .Where(x => !x.IsValid || x.Errors.Any())
                    .SelectMany(x => x.Errors);

                if (errors.Any())
                {
                    throw new ValidationException(errors);
                }
            }

            return await next();
        }
    }
}
