using System.Collections.Generic;
using Common.EnumList;
using Domain.Attributes;
using Domain.Attributes.Notification;
using Domain.Attributes.Store;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.Store;

[Auditable]
[MainEntity]
public class Product  : BaseEntityWithIdentityKey
{
    public string Title { get; set; }
    /// <summary>
    /// json: List<String>
    /// </summary>
    public string ImagePath { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public ProductTypeEnum ProductTypeEnum { get; set; }
    public decimal Price { get; set; }
    public float Inventory { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
}