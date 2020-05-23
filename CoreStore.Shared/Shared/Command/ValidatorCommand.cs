using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CoreStore.Shared.Command
{
    public class ValidatorCommand<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Result validations = new Result();

            if (request is ValidatableCommand validatable)
            {
                validatable.Validate();

                if (validatable.Invalid)
                {
                    foreach (var notification in validatable.Notifications)
                    {
                        validations.AddNotification(notification);
                    }

                    var result = validations as TResponse;

                    return result;
                }
            }

            TResponse response = await next();
            return response;
        }
    }
}
