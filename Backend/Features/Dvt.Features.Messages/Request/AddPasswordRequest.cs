using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Messages.Request
{
    public class AddPasswordRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
