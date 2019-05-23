using System;
using System.Collections.Generic;

namespace Dvt.Features.Core.Entities
{
    public partial class SystemFunction
    {
        public SystemFunction()
        {
            SystemProfileFunction = new HashSet<SystemProfileFunction>();
        }

        public int SystemFunctionId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int SystemFunctionGroupId { get; set; }

        public virtual SystemFunctionGroup SystemFunctionGroup { get; set; }
        public virtual ICollection<SystemProfileFunction> SystemProfileFunction { get; set; }
    }
}
