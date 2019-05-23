using System;
using System.Collections.Generic;

namespace Dvt.Features.Core.Entities
{
    public partial class SystemFunctionGroup
    {
        public SystemFunctionGroup()
        {
            SystemFunction = new HashSet<SystemFunction>();
        }

        public int SystemFunctionGroupId { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SystemFunction> SystemFunction { get; set; }
    }
}
