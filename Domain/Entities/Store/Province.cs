using System.Collections.Generic;
using Common.EnumList;
using Domain.Attributes;
using Domain.Attributes.Store;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.Store;

[Auditable]
[MainEntity]
public class Province : BaseEntityWithIdentityKey
{
    public string Title { get; set; }

    public ICollection<County> Counties { get; set; }
}

[Auditable]
[MainEntity]
public class County : BaseEntityWithIdentityKey
{
    public int ProvinceId { get; set; }
    public string Title { get; set; }

    public virtual Province Province { get; set; }
    public ICollection<Part> Parts { get; set; }
}

[Auditable]
[MainEntity]
public class Part : BaseEntityWithIdentityKey
{
    public int CountyId { get; set; }
    public string Title { get; set; }

    public virtual County County { get; set; }
    public ICollection<CityOrVillage> CityOrVillages { get; set; }
}

[Auditable]
[MainEntity]
public class CityOrVillage : BaseEntityWithIdentityKey
{
    public int PartId { get; set; }
    public string Title { get; set; }
    public LocationTypeEnum LocationType { get; set; }

    public virtual Part Part { get; set; }

    public ICollection<Order> Orders { get; set; }
}
