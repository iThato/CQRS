using System;
using System.Collections.Generic;

namespace Dvt.Features.Core.Entities
{
    public partial class Token
    {
        public int Id { get; set; }
        public Guid Value { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TokenTypeId { get; set; }
        public Guid UserId { get; set; }

        public virtual TokenType TokenType { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
