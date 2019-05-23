using System.Collections.Generic;
using Dvt.Infrastructure.Enums;

namespace Dvt.Infrastructure.Interfaces {
    public interface IPrincipal
    {
        string EmailAddress { get; }
        int Id { get; }
        IList<SystemFunction> SystemFunctions { get; }
    }
}
