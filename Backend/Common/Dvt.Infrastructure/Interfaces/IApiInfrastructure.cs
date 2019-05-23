using MediatR;

namespace Dvt.Infrastructure.Interfaces
{
    public interface IApiInfrastructure
    {
        IClock Clock { get; }
        IApiConfiguration ApiConfiguration { get; }
        IApplicationLogger ApplicationLogger { get; }

        IMediator Mediator { get; }
    }
}
