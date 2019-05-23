using System;
using System.Collections.Generic;

namespace Dvt.Features.Core.Entities
{
    public partial class SystemProfileFunction
    {
        public int SystemFunctionId { get; set; }
        public int SystemProfileId { get; set; }

        public virtual SystemFunction SystemFunction { get; set; }
        public virtual SystemProfile SystemProfile { get; set; }
    }
}
