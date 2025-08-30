using Common.Enum;
using Domain.Attributes;
using Domain.Attributes.Map;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.Map
{
    [MapEntity]
    [Auditable]
    public class Map : BaseEntityWithIdentityKey
    {
        public ProvinceEnum Province { get; set; }
        public string WebsiteAddress { get; set; }
        public string Description { get; set; }
    }
}