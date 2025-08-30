using System.Collections.Generic;
using Domain.Attributes;
using Domain.Attributes.Statement;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.Statement
{
    [Auditable]
    [StatementEntity]
    public class StatementCategory : BaseEntityWithIdentityKey
    {
        public string Title { get; set; }
        
        public virtual ICollection<Statement> Statements { get; set; }

    }
}