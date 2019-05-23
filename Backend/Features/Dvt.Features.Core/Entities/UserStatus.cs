using System;
using System.Collections.Generic;

namespace Dvt.Features.Core.Entities
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            UserAccount = new HashSet<UserAccount>();
        }

        public int UserStatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserAccount> UserAccount { get; set; }
    }
}
