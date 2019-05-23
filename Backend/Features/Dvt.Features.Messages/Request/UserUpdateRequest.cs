using System;
using System.ComponentModel.DataAnnotations;

namespace Dvt.Features.Messages.Request
{
    public class UserUpdateRequest
    {
        [Required]
        public Guid UserAccountId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string KnownAs { get; set; }

        public string ContactNumber { get; set; }

        public int SystemProfileId { get; set; }


    }
}
