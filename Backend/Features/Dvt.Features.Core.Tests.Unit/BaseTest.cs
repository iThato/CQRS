using MediatR;

namespace Dvt.Features.Core.Tests.Unit
{
    public class BaseTest
    {
        private const string _appPrefix = "Dvt";
        public readonly IMediator _mediator;
        public BaseTest()
        {
            _mediator = Config.GetMediator();

            //var builder = new ContainerBuilder();
            //InfrastructureBootstrap.InitialiseScanning(builder, _appPrefix);
        }
    }
}
