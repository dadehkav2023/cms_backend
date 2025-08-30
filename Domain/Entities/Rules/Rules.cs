using System.Collections.Generic;
using Common.Enum;
using Domain.Attributes;
using Domain.Attributes.Rules;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.Rules
{
    [RulesEntity]
    [Auditable]
    public class Rules : BaseEntityWithIdentityKey
    {
        public string Description { get; set; }

        public virtual ICollection<RulesAttachment> RulesAttachments { get; set; }
    }

    [RulesEntity]
    [Auditable]
    public class RulesAttachment : BaseEntityWithIdentityKey
    {
        public string FilePath { get; set; }
        public string Title { get; set; }
        public FileTypeEnum FileType { get; set; }

        public int RulesId { get; set; }
        public virtual Rules Rules { get; set; }
    }
}