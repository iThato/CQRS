using System;
using System.Collections.Generic;

namespace Dvt.Features.Core.Entities
{
    public partial class TokenType
    {
        public TokenType()
        {
            Token = new HashSet<Token>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Token> Token { get; set; }
    }
}
