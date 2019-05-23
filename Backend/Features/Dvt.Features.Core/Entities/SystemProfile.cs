using System;
using System.Collections.Generic;

namespace Dvt.Features.Core.Entities
{
    public partial class SystemProfile
    {
        public SystemProfile()
        {
            SystemProfileFunction = new HashSet<SystemProfileFunction>();
            UserAccount = new HashSet<UserAccount>();
        }

        public int SystemProfileId { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<SystemProfileFunction> SystemProfileFunction { get; set; }
        public virtual ICollection<UserAccount> UserAccount { get; set; }
    }
}
