using System.Linq;
using Dvt.Common.Structures;
using Dvt.Features.Core.Entities;
using Dvt.Infrastructure.Interfaces;

namespace Dvt.Features.Core.Features
{
    public abstract class RequestHandlerInfrastructureBase
    {
        // ReSharper disable once InconsistentNaming
        protected IApiInfrastructure ApiInfrastructure { get; }

        protected RequestHandlerInfrastructureBase(IApiInfrastructure apiInfrastructure)
        {
            ApiInfrastructure = apiInfrastructure;
        }

        protected IApplicationLogger ApplicationLogger => ApiInfrastructure.ApplicationLogger;
        protected IApiConfiguration ApiConfiguration => ApiInfrastructure.ApiConfiguration;

        public ValidationError PerformConcurrencyCheck(byte[] token1, byte[] token2)
        {
            if (token1.SequenceEqual(token2)) return null;
            var error = new ValidationError
            {
                ErrorMessage =
                    "The data you are working on has been modified by another process. Please refresh the page to have the latest data."
            };
            return error;

        }
    }

    public abstract class RequestHandlerBase : RequestHandlerInfrastructureBase
    {
        protected DvtDatabaseContext Database { get; }

        protected RequestHandlerBase(IApiInfrastructure apiInfrastructure, DvtDatabaseContext context) : base(apiInfrastructure)
        {

            Database = context;
        }
    }
}
