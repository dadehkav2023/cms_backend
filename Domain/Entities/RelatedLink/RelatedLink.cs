using Domain.Attributes;
using Domain.Attributes.RelatedLink;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.RelatedLink
{
    [RelatedLinkEntity]
    [Auditable]

    public class RelatedLink : BaseEntityWithIdentityKey
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public bool IsActive { get; set; }
    }
}