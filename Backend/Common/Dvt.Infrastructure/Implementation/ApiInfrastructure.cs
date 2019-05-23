using System;
using Dvt.Infrastructure.Interfaces;
using MediatR;

namespace Dvt.Infrastructure.Implementation
{
    // Note: This class is not a singleton as there might be items on the API in the future that need to be scoped differently
    public class ApiInfrastructure : IApiInfrastructure
    {
        private readonly Lazy<IApiConfiguration> _apiConfiguration;
        private readonly Lazy<IApplicationLogger> _applicationLogger;
        private readonly Lazy<IClock> _clock;

        public ApiInfrastructure(Lazy<IClock> clock, Lazy<IApiConfiguration> apiConfiguration,
            Lazy<IApplicationLogger> applicationLogger, IMediator mediator)
        {
            _clock = clock;
            _apiConfiguration = apiConfiguration;
            _applicationLogger = applicationLogger;
            Mediator = mediator;
        }

        public IApplicationLogger ApplicationLogger => _applicationLogger.Value;
        public IMediator Mediator { get; }

        public IApiConfiguration ApiConfiguration => _apiConfiguration.Value;
        public IClock Clock => _clock.Value;
    }
}
