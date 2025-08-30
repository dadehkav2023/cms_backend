using Domain.Attributes;
using Domain.Attributes.QuickAccess;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.QuickAccess
{
    [QuickAccessEntity]
    [Auditable]
    public class QuickAccess : BaseEntityWithIdentityKey
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public bool IsActive { get; set; }
    }
}