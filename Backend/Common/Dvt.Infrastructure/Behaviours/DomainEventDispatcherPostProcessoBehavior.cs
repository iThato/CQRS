using System.Threading.Tasks;
using Dvt.Common.Extensions;
using Dvt.Infrastructure.Structures;
using MediatR;
using MediatR.Pipeline;

namespace Dvt.Infrastructure.Behaviours
{
    public class DomainEventDispatcherPostProcessoBehavior<TRequest, TResponse>
        : IRequestPostProcessor<TRequest, TResponse>
    {
        public IMediator Mediator { get; }

        public DomainEventDispatcherPostProcessoBehavior(IMediator mediator)
        {
            Mediator = mediator;
        }

        public Task Process(TRequest request, TResponse response)
        {
            if (response is OperationResult result && result.DomainEventsList.HasItems())
            {
                result.DomainEventsList.ForEach(
                    e =>  Mediator.Publish(e));
            }

            return Task.FromResult(true);
        }
    }
}
