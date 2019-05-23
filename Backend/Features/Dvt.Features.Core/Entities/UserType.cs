using System;
using System.Collections.Generic;

namespace Dvt.Features.Core.Entities
{
    public partial class UserType
    {
        /**
         * Extends AbstractSet and implements the Set interface.
        **/
        public UserType()
        {
            UserAccount = new HashSet<UserAccount>();
        }

        public int UserTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserAccount> UserAccount { get; set; }
    }
}
