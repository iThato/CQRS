using System;
using System.Collections.Generic;

namespace Dvt.Features.Core.Entities
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            Token = new HashSet<Token>();
        }

        public Guid UserAccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string KnownAs { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool AcceptedTerms { get; set; }
        public int UserStatusId { get; set; }
        public int SystemProfileId { get; set; }

        public virtual SystemProfile SystemProfile { get; set; }
        public virtual UserStatus UserStatus { get; set; }
        public virtual ICollection<Token> Token { get; set; }
    }
}
