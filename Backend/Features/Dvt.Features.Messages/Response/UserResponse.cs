using System;
using System.Collections.Generic;

namespace Dvt.Features.Messages.Response
{
    public class UserResponse
    {
        public Guid UserAccountId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string KnownAs { get; set; }

        public string Username { get; set; }

        public string ContactNumber { get; set; }

        public int? SystemProfileId { get; set; }

        public bool AcceptedTerms { get; set; }

        public string SystemProfile { get; set; }

        public List<int> SystemFunctions { get; set; }

        public List<UserResponse> result { get; set; }
    }
}
